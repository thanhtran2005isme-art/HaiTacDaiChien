#!/usr/bin/env python3
"""Fail-closed original IL2CPP v31 metadata class/method ownership inventory.

This parses original XAPK global-metadata.dat only. A managed method definition
is NOT evidence of the address, instructions, arguments, or runtime expression
in libil2cpp.so. No method is interpreted or approximated from a name.
"""
from __future__ import annotations

import hashlib
import re
import struct

MAGIC = 0xFAB11BAF
VERSION = 31
MAX_FILE = 128 * 1024 * 1024
# Unity 2022.3 metadata v31: header offset/byte-size pairs and struct strides.
STRINGS = 0x18
METHODS = 0x30
TYPES = 0xA0
METHOD_STRIDE = 0x24
TYPE_STRIDE = 0x58
TYPE_METHOD_START = 0x24
TYPE_METHOD_COUNT = 0x40
NO_METHODS = 0xFFFFFFFF
TARGET = re.compile(r"^PanelHome2(?:$|[A-Za-z0-9_+].*)$")


class MetadataBlocked(ValueError):
    """Unable to prove v31 method ownership from original source data."""


def u32(blob, pos):
    if pos < 0 or pos + 4 > len(blob):
        raise MetadataBlocked("Metadata header or definition truncated")
    return struct.unpack_from("<I", blob, pos)[0]


def u16(blob, pos):
    if pos < 0 or pos + 2 > len(blob):
        raise MetadataBlocked("Metadata definition truncated")
    return struct.unpack_from("<H", blob, pos)[0]


def table(blob, at, stride=1):
    offset, length = u32(blob, at), u32(blob, at + 4)
    if (length == 0 or length % stride or offset < 0x100 or
            offset + length > len(blob) or offset + length < offset):
        raise MetadataBlocked("Original IL2CPP metadata section invalid")
    return offset, length // stride


def source_string(data, base, length, index, *, allow_empty=False):
    if index >= length:
        raise MetadataBlocked("Original metadata string index outside name table")
    end = data.find(b"\0", base + index, min(base + length, base + index + 257))
    if end == -1:
        raise MetadataBlocked("Unterminated or oversized metadata identifier")
    value = data[base + index:end].decode("utf-8", "strict")
    if (not value and not allow_empty) or len(value) > 256:
        raise MetadataBlocked("Invalid metadata identifier")
    return value


def is_p2_type(name, namespace=""):
    if name == "SafeAreaAdapter" or bool(TARGET.fullmatch(name)):
        return True
    return (name in {"Canvas", "CanvasScaler", "RectTransform", "Screen"} and
            namespace in {"UnityEngine", "UnityEngine.UI"})


def inspect(data: bytes):
    if not isinstance(data, bytes) or not 0x200 <= len(data) <= MAX_FILE:
        raise MetadataBlocked("Original metadata unavailable or oversized")
    if u32(data, 0) != MAGIC or u32(data, 4) != VERSION:
        raise MetadataBlocked("Expected exact IL2CPP metadata v31 header")
    string_base, string_bytes = table(data, STRINGS)
    method_base, method_count = table(data, METHODS, METHOD_STRIDE)
    type_base, type_count = table(data, TYPES, TYPE_STRIDE)
    if method_count > 2_000_000 or type_count > 400_000:
        raise MetadataBlocked("Untrusted metadata table counts")
    # Method start/length refer to global metadata indexes, not ELF offsets.
    classes = []
    for ti in range(type_count):
        off = type_base + ti * TYPE_STRIDE
        name = source_string(data, string_base, string_bytes, u32(data, off),
                             allow_empty=True)
        if not name:
            continue
        namespace_index = u32(data, off + 4)
        namespace = (source_string(data, string_base, string_bytes, namespace_index)
                     if namespace_index else "")
        if not is_p2_type(name, namespace):
            continue
        start = u32(data, off + TYPE_METHOD_START)
        count = u16(data, off + TYPE_METHOD_COUNT)
        if count and (start == NO_METHODS or start + count > method_count):
            raise MetadataBlocked("Original class method range invalid")
        rows = []
        for mi in range(start, start + count) if count else ():
            method_off = method_base + mi * METHOD_STRIDE
            declaring_type = u32(data, method_off + 4)
            if declaring_type != ti:
                raise MetadataBlocked("IL2CPP method declaring type conflicts with owner")
            method_name = source_string(
                data, string_base, string_bytes, u32(data, method_off))
            if len(method_name) > 160 or not all(ch.isprintable() for ch in method_name):
                raise MetadataBlocked("IL2CPP metadata method name invalid")
            token = u32(data, method_off + 0x18)
            if token >> 24 != 0x06:
                raise MetadataBlocked("IL2CPP method-definition token kind unverified")
            rows.append({"name": method_name, "methodDefinitionIndex": mi,
                         "methodToken": f"0x{token:08x}",
                         "sourceDefinitionOffset": method_off,
                         "declaringTypeIndex": ti,
                         "nativeAddress": None,
                         "methodBodyVerified": False})
        classes.append({
            "className": name, "namespace": namespace, "typeDefinitionIndex": ti,
            "typeDefinitionSourceOffset": off, "methodStart": start if count else None,
            "methodCount": count, "methods": rows,
            "nativeMethodAddressResolved": False,
            "runtimeExpressionProven": False,
        })
    if len({r["typeDefinitionIndex"] for r in classes}) != len(classes):
        raise MetadataBlocked("Duplicate original class definition index")
    return {"classification": "REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING",
            "metadataVersion": VERSION,
            "metadataSha256": hashlib.sha256(data).hexdigest(),
            "metadataByteLength": len(data),
            "totalTypeDefinitions": type_count,
            "totalMethodDefinitions": method_count,
            "safeAreaClassDefinitions": sum(x["className"] == "SafeAreaAdapter"
                                            for x in classes),
            "panelHome2ClassDefinitions": sum(x["className"].startswith("PanelHome2")
                                              for x in classes),
            "targetClassDefinitions": classes,
            "targetTypesWithMetadataOnlyEvidence": len(classes),
            "methodCodeAddressesResolved": 0, "runtimeFormulaRecovered": False,
            "unityAssetsChanged": False, "unityImportAllowed": False}


def check_library_elf(library: bytes):
    if not isinstance(library, bytes) or len(library) < 64:
        raise MetadataBlocked("Original libil2cpp.so unavailable")
    if library[:6] != b"\x7fELF\x02\x01" or u16(library, 18) != 183:
        raise MetadataBlocked("Original libil2cpp.so must be little-endian ARM64 ELF")
    return {"sha256": hashlib.sha256(library).hexdigest(),
            "byteLength": len(library), "machine": "AARCH64_ELF64",
            "methodPointerMappingVerified": False, "methodBodiesDisassembled": False}
