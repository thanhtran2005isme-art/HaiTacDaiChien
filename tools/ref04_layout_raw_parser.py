#!/usr/bin/env python3
"""Bounded independent raw-byte walk for a supplied Unity TypeTree.

The walk is independent of UnityPy's C++ TypeTree value decoder, NOT of the
IL2CPP-derived schema supplied to it. It may confirm exact raw byte spans and
alignment under that schema, but cannot independently prove the schema itself.
Never use this to authorize import or infer original runtime layout.
"""
from __future__ import annotations

import hashlib
import math
import struct

ALIGN_FLAG = 0x4000
MAX_NODES = 20000
MAX_ARRAY = 100000
MAX_DEPTH = 96
MAX_BYTES = 2 * 1024 * 1024

PRIMITIVES = {
    "bool": ("?", 1), "Boolean": ("?", 1),
    "char": ("b", 1), "SInt8": ("b", 1), "sbyte": ("b", 1),
    "UInt8": ("B", 1), "unsigned char": ("B", 1), "byte": ("B", 1),
    "short": ("h", 2), "SInt16": ("h", 2),
    "UInt16": ("H", 2), "unsigned short": ("H", 2),
    "int": ("i", 4), "SInt32": ("i", 4),
    "UInt32": ("I", 4), "unsigned int": ("I", 4),
    "long long": ("q", 8), "SInt64": ("q", 8),
    "UInt64": ("Q", 8), "unsigned long long": ("Q", 8),
    "float": ("f", 4), "double": ("d", 8),
}


class RawWalkBlocked(ValueError):
    """A source byte/layout invariant failed; no field values may be promoted."""


def byte_order(reader):
    """Never assume little-endian; use the source serialized file's reader."""
    source = getattr(getattr(reader, "reader", None), "endian", None)
    if source in ("<", "little"):
        return "<"
    if source in (">", "big"):
        return ">"
    raise RawWalkBlocked("Original source byte order unavailable")


class Cursor:
    def __init__(self, raw: bytes, endian: str):
        if not isinstance(raw, bytes) or not raw or len(raw) > MAX_BYTES:
            raise RawWalkBlocked("Original serialized object invalid or oversized")
        if endian not in ("<", ">"):
            raise RawWalkBlocked("Original serialized byte order unknown")
        self.raw = raw
        self.endian = endian
        self.pos = 0
        self.nodes = 0

    def get(self, amount):
        if amount < 0 or self.pos + amount > len(self.raw):
            raise RawWalkBlocked("Serialized object ended before schema field")
        data = self.raw[self.pos:self.pos + amount]
        self.pos += amount
        return data

    def scalar(self, fmt, size):
        return struct.unpack(self.endian + fmt, self.get(size))[0]

    def align(self):
        target = (self.pos + 3) & ~3
        self.get(target - self.pos)

    def walk(self, node, depth=0):
        self.nodes += 1
        if self.nodes > MAX_NODES or depth > MAX_DEPTH:
            raise RawWalkBlocked("TypeTree depth/node budget exceeded")
        children = getattr(node, "m_Children", None)
        typ = getattr(node, "m_Type", None)
        if children is None or not isinstance(typ, str):
            raise RawWalkBlocked("Incomplete source TypeTree node")
        start = self.pos
        if typ in PRIMITIVES and not children:
            value = self.scalar(*PRIMITIVES[typ])
        elif typ == "string":
            length = self.scalar("i", 4)
            if length < 0 or length > MAX_BYTES:
                raise RawWalkBlocked("Source string byte length invalid")
            raw = self.get(length)
            value = raw.decode("utf-8", errors="surrogateescape")
            self.align()
        elif len(children) == 1 and getattr(children[0], "m_Type", None) == "Array":
            arr = children[0]
            items = getattr(arr, "m_Children", None)
            if not items or len(items) != 2 or getattr(items[0], "m_Name", None) != "size":
                raise RawWalkBlocked("Unsupported source array schema")
            length = self.scalar("i", 4)
            if length < 0 or length > MAX_ARRAY:
                raise RawWalkBlocked("Source array element count invalid")
            value = [self.walk(items[1], depth + 1) for _ in range(length)]
            if int(getattr(arr, "m_MetaFlag", 0) or 0) & ALIGN_FLAG:
                self.align()
        elif children:
            value = {}
            for child in children:
                name = getattr(child, "m_Name", None)
                if not isinstance(name, str) or name in value:
                    raise RawWalkBlocked("Ambiguous duplicate source schema field")
                value[name] = self.walk(child, depth + 1)
        else:
            raise RawWalkBlocked("Unrecognized source field type: " + str(typ))
        if int(getattr(node, "m_MetaFlag", 0) or 0) & ALIGN_FLAG:
            self.align()
        if self.pos <= start:
            raise RawWalkBlocked("Source schema field consumed no bytes")
        return value


def reparse_strict(raw, schema, expected, *, endian):
    """Reparse an entire MonoBehaviour with explicit offsets and no partial reads.

    expected maps only allowlisted fields to original AssetStudio strict values.
    Status remains derived-schema RAW_REPARSE_REVIEW_ONLY even when all match.
    """
    if not isinstance(expected, dict) or not expected:
        raise RawWalkBlocked("No source LayoutGroup comparison fields")
    c = Cursor(raw, endian)
    members = getattr(schema, "m_Children", None)
    if not members:
        raise RawWalkBlocked("Source MonoBehaviour root has no members")
    decoded = {}
    spans = {}
    for child in members:
        key = getattr(child, "m_Name", None)
        if not isinstance(key, str) or key in decoded:
            raise RawWalkBlocked("Source MonoBehaviour field collision")
        start = c.pos
        decoded[key] = c.walk(child, depth=1)
        end = c.pos
        if key in expected:
            spans[key] = {
                "offset": start, "length": end - start,
                "rawFieldBytesSha256": hashlib.sha256(raw[start:end]).hexdigest(),
            }
    if c.pos != len(raw):
        raise RawWalkBlocked("Raw parser did not consume exact source object bytes")
    if set(expected) - set(decoded):
        raise RawWalkBlocked("Source managed field missing in raw replay")
    if len(spans) != len(expected):
        raise RawWalkBlocked("Incomplete source raw field span accounting")
    for key, verified in expected.items():
        observed = decoded[key]
        if type(observed) is not type(verified) or not equal_exact(observed, verified):
            raise RawWalkBlocked("Raw field differs from original strict source: " + key)
    return {"nativeHeader": {k: decoded.get(k) for k in
                             ("m_GameObject", "m_Script", "m_Enabled")},
            "fieldByteSpans": spans,
            "fullObjectBytes": len(raw),
            "fullObjectSha256": hashlib.sha256(raw).hexdigest(),
            "derivedSchemaOnly": True,
            "unityImportAllowed": False,
            "runtimeLayoutProven": False}


def equal_exact(a, b):
    if type(a) is not type(b):
        return False
    if isinstance(a, dict):
        return set(a) == set(b) and all(equal_exact(a[k], b[k]) for k in a)
    if isinstance(a, list):
        return len(a) == len(b) and all(equal_exact(x, y) for x, y in zip(a, b))
    if isinstance(a, float):
        return math.isfinite(a) and math.isfinite(b) and a == b
    return a == b
