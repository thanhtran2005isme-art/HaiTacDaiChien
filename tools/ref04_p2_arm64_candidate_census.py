#!/usr/bin/env python3
"""Source-bounded native ARM64 entry-block census; NOT UI formula recovery.

A script.json method-label-to-address candidate is NOT independently verified
method ownership. Capstone is used only to inspect bounded native instruction
categories and report evidence for later control-flow validation; never
manufacture Screen.safeArea, CanvasScaler, device viewport or Unity coordinates.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import re
from pathlib import Path

import recover_managed_ui_fields as source
import ref04_arm64_elf_regions as elf

ROOT=Path(__file__).resolve().parents[1]
STATUS="REF04_P2_ARM64_CANDIDATE_ENTRY_BLOCKS_DISASSEMBLED_NO_FORMULA"
MAX_BYTES=256

DIRECT_BRANCH = re.compile(r"^(?:#)?0x([0-9a-fA-F]{1,16})$")

def bounded_source_branch(insn, regions):
    """Inspect an immediate branch operand; it is NOT a verified call edge.

    The decoded instruction is source-backed, but its containing method and
    target method ownership are not yet independently proven.
    """
    opcode = insn.mnemonic.lower()
    if opcode not in ("b", "bl", "cbz", "cbnz", "tbz", "tbnz") and not opcode.startswith("b."):
        return None
    operand = getattr(insn, "op_str", None)
    if not isinstance(operand, str) or len(operand) > 160:
        return {"kind": opcode, "status": "BLOCKED_BRANCH_OPERAND_UNREADABLE"}
    last = operand.rsplit(",", 1)[-1].strip()
    match = DIRECT_BRANCH.fullmatch(last)
    if not match:
        return {"kind": opcode, "status": "BLOCKED_NONCANONICAL_IMMEDIATE"}
    target = int(match.group(1), 16)
    if target % 4:
        return {"kind": opcode, "status": "BLOCKED_UNALIGNED_TARGET"}
    try:
        source_offset = elf.checked_original_offset(regions, target)
    except elf.ElfBlocked:
        return {"kind": opcode, "status": "TARGET_NOT_IN_ORIGINAL_EXECUTABLE_FILE_BYTES"}
    return {"kind": opcode, "status": "SOURCE_EXECUTABLE_TARGET_CANDIDATE",
            "targetELFVirtualAddress": target, "targetOriginalELFFileOffset": source_offset,
            "nativeTargetMethodOwnershipProven": False}


def inspect_entry(candidate, binary, regions, disassembler, neighbor=None):
    if candidate.get("methodOwnershipIndependentlyProven") is not False or (
        candidate.get("runtimeExpressionProven") is not False or
        candidate.get("fullMethodBodyVerified") is not False):
        raise ValueError("Native address candidate falsely promoted as verified method")
    addr=candidate["candidateELFVirtualAddress"]
    offset=elf.checked_original_offset(regions,addr)
    if addr%4 or offset != candidate["originalELFFileOffset"]:
        raise ValueError("Original ARM64 address and file-offset conflict")
    if hashlib.sha256(binary[offset:offset+16]).hexdigest() != (
        candidate["first16ExecutableBytesSha256"]):
        raise ValueError("Original method source bytes SHA changed")
    seg=next((x for x in regions["originalLoadSegments"]
             if x["executable"] and x["virtualAddressStart"]<=addr<
             x["virtualAddressStart"]+x["fileByteLength"]),None)
    if seg is None:
        raise ValueError("Entry point not in original executable file bytes")
    size=min(MAX_BYTES,seg["fileOffsetStart"]+seg["fileByteLength"]-offset)
    if neighbor is not None:
        if type(neighbor) is not int or neighbor<=addr or neighbor%4:
            raise ValueError("Invalid executable code candidate neighbor")
        size=min(size,neighbor-addr)
    size=(size//4)*4
    if size<4:
        raise ValueError("Not enough real executable bytes for ARM64 instruction")
    mnemonic_counts=collections.Counter()
    instruction_count=0
    terminated=False
    source_branch_candidates=[]
    for insn in disassembler(binary[offset:offset+size],addr):
        if insn.address != addr+instruction_count*4 or insn.size!=4:
            raise ValueError("Non-contiguous or invalid ARM64 instruction decoding")
        op=insn.mnemonic
        if not isinstance(op,str) or not op or not op.isascii():
            raise ValueError("Unknown ARM64 opcode mnemonic")
        mnemonic_counts[op]+=1
        edge=bounded_source_branch(insn,regions)
        if edge is not None:
            edge["originalInstructionAddress"]=insn.address
            source_branch_candidates.append(edge)
        instruction_count+=1
        if op in ("ret","br","eret"):
            terminated=True
            break
        if instruction_count >= MAX_BYTES//4:
            break
    if instruction_count==0:
        raise ValueError("No native ARM64 instruction decoded at entrypoint")
    return {
        "sourceMethodDefinitionIndex":candidate["originalMethodDefinitionIndex"],
        "sourceMethodToken":candidate["sourceMethodToken"],
        "candidateELFVirtualAddress":addr,
        "originalELFFileOffset":offset,
        "sourceFirst16Sha256":candidate["first16ExecutableBytesSha256"],
        "inspectedOriginalBytes":instruction_count*4,
        "instructionCount":instruction_count,
        "instructionMnemonicHistogram":dict(sorted(mnemonic_counts.items())),
        "entryTerminatorEncountered":terminated,
        "boundedSourceBranchCandidates":source_branch_candidates,
        "independentNativeMethodCallGraphProven":False,
        "methodOwnershipIndependentlyProven":False,
        "completeNativeMethodBodyProven":False,
        "screenSafeAreaOrCanvasFormulaProven":False,
        "unityImportAllowed":False,
    }

def audit(binary, regions, candidates_report, decoder):
    if (hashlib.sha256(binary).hexdigest() !=
          regions.get("originalLibrarySha256") or
        candidates_report.get("sourceELFLibrarySha256") !=
          regions.get("originalLibrarySha256") or
        candidates_report.get("nativeCodeMethodOwnershipVerified") is not False or
        candidates_report.get("runtimeFormulaRecovered") is not False or
        candidates_report.get("classification") !=
            "REF04_P2_NATIVE_METHOD_ADDRESS_CANDIDATES_NEED_REVIEW"):
        raise ValueError("Exact XAPK native binary candidate provenance mismatch")
    entries=candidates_report.get("candidates",[])
    if len(entries)!=candidates_report.get("strictCandidateCount"):
        raise ValueError("Native source candidate inventory count mismatch")
    known_addresses=sorted({x["candidateELFVirtualAddress"] for x in entries})
    rows=[]
    for c in entries:
        addr=c["candidateELFVirtualAddress"]
        idx=known_addresses.index(addr)
        # Known method entry boundaries only; do not assume a method ends there.
        nxt=known_addresses[idx+1] if idx+1<len(known_addresses) else None
        rows.append(inspect_entry(c,binary,regions,decoder,nxt))
    return {
        "classification":STATUS,
        "originalLibSha256":regions["originalLibrarySha256"],
        "inspectedCandidateEntryPoints":len(rows),
        "originalMethodAddressesMappedIndependently":0,
        "completeNativeBodiesProven":0,
        "sourceBranchInstructionsObserved":sum(len(x["boundedSourceBranchCandidates"]) for x in rows),
        "independentMethodCallEdgesProven":0,
        "runtimeAlignmentFormula":None,
        "runtimeAlignmentProven":False,
        "runtimeSafeAreaProven":False,
        "originalSourceOnlyInstructionCensus":rows,
        "sourceFieldApplicationAllowed":False,
        "unityAssetsChanged":False,
    }

def execute(root=ROOT):
    try:
        from capstone import Cs,CS_ARCH_ARM64,CS_MODE_ARM
    except ImportError as exc:
        raise RuntimeError("Pinned Capstone ARM64 dependency required") from exc
    xapk=next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None:
        raise FileNotFoundError("Original XAPK missing")
    pair=source.read_source_pair(xapk)
    binary=pair["library"][0]
    regions=elf.elf_regions(binary)
    path=root/"output/ref04-p2-native-code-address-candidates.json"
    raw=json.loads(path.read_text(encoding="utf-8"))
    md=Cs(CS_ARCH_ARM64,CS_MODE_ARM)
    md.detail=False
    report=audit(binary,regions,raw,lambda bits,va:md.disasm(bits,va))
    dest=root/"output/ref04-p2-arm64-candidate-instruction-census.json"
    tmp=dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(report,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({
        "classification":STATUS,
        "nativeEntryBlocksInspected":report["inspectedCandidateEntryPoints"],
        "completeNativeBodiesProven":0,
        "sourceBranchInstructionsObserved":report["sourceBranchInstructionsObserved"],
        "independentMethodCallEdgesProven":0,
        "runtimeAlignmentProven":False,
        "sourceAssetsChanged":False,
    },sort_keys=True))
    return report

if __name__=="__main__":
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root",type=Path,default=ROOT)
    args=parser.parse_args()
    execute(args.root.resolve())
