#!/usr/bin/env python3
"""Original ARM64 ELF executable-region provenance, not a method decompiler.

Metadata method tokens do not encode native function addresses. This tool
verifies ELF64 load segments and executable virtual addresses without assuming
a token-to-address map, device mapping, base slide, or Unity UI formula.
"""
from __future__ import annotations

import hashlib
import struct

PT_LOAD = 1
PF_X = 1
PF_R = 4
MAX_HEADERS = 256

class ElfBlocked(ValueError):
    pass

def elf_regions(blob):
    if not isinstance(blob, bytes) or len(blob) < 64:
        raise ElfBlocked("Original ELF too small")
    if blob[:6] != b"\x7fELF\x02\x01" or blob[6] != 1:
        raise ElfBlocked("Expected original ELF64 little-endian current version")
    machine = struct.unpack_from("<H",blob,18)[0]
    if machine != 183:
        raise ElfBlocked("Original ELF not AArch64")
    ehsize = struct.unpack_from("<H",blob,52)[0]
    phoff = struct.unpack_from("<Q",blob,32)[0]
    phentsize, phnum = struct.unpack_from("<HH",blob,54)
    if (ehsize != 64 or phentsize != 56 or not 0 < phnum <= MAX_HEADERS or
        phoff < 64 or phoff + phentsize * phnum > len(blob)):
        raise ElfBlocked("Invalid ELF64 program header table")
    seen_file = []
    load = []
    for i in range(phnum):
        base = phoff + i * phentsize
        kind, flags = struct.unpack_from("<II",blob,base)
        offset, vaddr, _, filesz, memsz, align = struct.unpack_from(
            "<QQQQQQ", blob, base + 8)
        if kind != PT_LOAD:
            continue
        if (filesz > memsz or offset + filesz > len(blob) or
            vaddr + memsz >= (1<<64) or
            align and ((align & (align-1)) != 0 or
                       (offset-vaddr) % align != 0)):
            raise ElfBlocked("Invalid original ELF load segment layout")
        if filesz:
            if any(not (offset + filesz <= lo or hi <= offset)
                   for lo,hi in seen_file):
                raise ElfBlocked("Overlapping ELF file load spans")
            seen_file.append((offset,offset+filesz))
        load.append({
            "virtualAddressStart":vaddr, "virtualAddressEnd":vaddr+memsz,
            "fileOffsetStart":offset, "fileByteLength":filesz,
            "executable":bool(flags&PF_X), "readable":bool(flags&PF_R),
            "originalFileBytesSha256":hashlib.sha256(
                blob[offset:offset+filesz]).hexdigest(),
        })
    if not load or not any(x["executable"] and x["fileByteLength"] for x in load):
        raise ElfBlocked("No original executable ELF load segment")
    return {
        "classification":"ORIGINAL_AARCH64_ELF_EXECUTABLE_REGION_SOURCE_ONLY",
        "originalLibrarySha256":hashlib.sha256(blob).hexdigest(),
        "elfProgramHeaderCount":phnum,
        "originalLoadSegments":load,
        "executableLoadSegments":sum(x["executable"] for x in load),
        "methodTokenToNativeAddressProven":False,
        "methodBodiesDecoded":False,
        "runtimeAlignmentFormulaProven":False,
        "unityImportAllowed":False,
    }

def checked_original_offset(report, virtual_address):
    """Map only a known *verified* ELF virtual address, not any method token."""
    if type(virtual_address) is not int or virtual_address < 0:
        raise ElfBlocked("Unverified virtual address")
    candidates=[]
    for row in report["originalLoadSegments"]:
        start=row["virtualAddressStart"]
        length=row["fileByteLength"]
        if row["executable"] and start <= virtual_address < start+length:
            candidates.append(row["fileOffsetStart"]+virtual_address-start)
    if len(candidates)!=1:
        raise ElfBlocked("No unique executable source file range for address")
    return candidates[0]
