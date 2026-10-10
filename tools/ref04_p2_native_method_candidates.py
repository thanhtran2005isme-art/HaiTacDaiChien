#!/usr/bin/env python3
"""Validate optional IL2CPP native *address candidates* against exact ELF.

A third-party dumper's script.json alone does not prove native method ownership,
control-flow, safe-area math, or runtime UI. This report can only justify bounded
follow-up disassembly at candidate ELF offsets. Never auto-apply to Unity.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
from pathlib import Path

import recover_managed_ui_fields as source
import ref04_arm64_elf_regions as elf
import ref04_il2cpp_method_index_v31 as metadata

ROOT=Path(__file__).resolve().parents[1]
MAX_SCRIPT_BYTES=200*1024*1024

def source_script_candidates(doc, method_index, exe, binary):
    if (not isinstance(doc,dict) or
        not isinstance(doc.get("ScriptMethod"),list) or
        method_index.get("classification")!=
          "REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING" or
        exe.get("originalLibrarySha256")!=hashlib.sha256(binary).hexdigest()):
        raise ValueError("Original binary/source dumper method index incompatible")
    source_methods=collections.defaultdict(list)
    for cls in method_index["targetClassDefinitions"]:
        key=(cls["namespace"]+"." if cls["namespace"] else "")+cls["className"]
        for method in cls["methods"]:
            source_methods[(key,method["name"])].append(method)
    by_name=collections.defaultdict(list)
    rows=doc["ScriptMethod"]
    if len(rows)>2_000_000:
        raise ValueError("Untrusted native script output method count")
    for row in rows:
        name=row.get("Name") if isinstance(row,dict) else None
        address=row.get("Address") if isinstance(row,dict) else None
        if not isinstance(name,str) or not isinstance(address,int) or (
            address<=0 or address>=(1<<63) or len(name)>256):
            continue
        by_name[name].append(address)
    candidates=[]
    blockers=collections.Counter()
    for (cls,method), definitions in sorted(source_methods.items()):
        label=cls+"$$"+method
        scripts=by_name.get(label,[])
        if len(definitions)!=1 or len(scripts)!=1:
            blockers["NOT_UNIQUE_METHOD_DEFINITION_OR_SCRIPT_MATCH"]+=1
            continue
        addr=scripts[0]
        if addr%4:
            blockers["NON_AARCH64_ALIGNED_ADDRESS"]+=1
            continue
        try:
            off=elf.checked_original_offset(exe,addr)
        except elf.ElfBlocked:
            blockers["ADDRESS_OUTSIDE_FILE_BACKED_EXECUTABLE_SEGMENT"]+=1
            continue
        if off+16>len(binary):
            blockers["SHORT_EXECUTABLE_CODE_BYTES"]+=1
            continue
        candidates.append({
            "className":cls,"methodName":method,
            "originalMethodDefinitionIndex":definitions[0]["methodDefinitionIndex"],
            "sourceMethodToken":definitions[0]["methodToken"],
            "candidateELFVirtualAddress":addr,
            "originalELFFileOffset":off,
            "first16ExecutableBytesSha256":hashlib.sha256(binary[off:off+16]).hexdigest(),
            "addressSource":"THIRD_PARTY_DUMPER_SCRIPT_NAME_NOT_INDEPENDENTLY_VERIFIED",
            "methodOwnershipIndependentlyProven":False,
            "fullMethodBodyVerified":False,
            "runtimeExpressionProven":False,
            "unityImportAllowed":False,
        })
    return {
        "classification":"REF04_P2_NATIVE_METHOD_ADDRESS_CANDIDATES_NEED_REVIEW",
        "sourceELFLibrarySha256":hashlib.sha256(binary).hexdigest(),
        "sourceMetadataSha256":method_index["metadataSha256"],
        "thirdPartyScriptJsonSha256":None,
        "strictCandidateCount":len(candidates),
        "unresolvedCountByReason":dict(sorted(blockers.items())),
        "candidates":candidates,
        "nativeCodeMethodOwnershipVerified":False,
        "runtimeFormulaRecovered":False,
        "sourceFieldApplicationAllowed":False,
        "unityAssetsChanged":False,
    }

def execute(root=ROOT,script=None):
    xapk=next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None: raise FileNotFoundError("Original XAPK missing")
    if script is None or not script.is_file() or script.stat().st_size>MAX_SCRIPT_BYTES:
        raise ValueError("Need bounded local third-party script.json (never commit)")
    binary_pair=source.read_source_pair(xapk)
    library=binary_pair["library"][0]
    meta=metadata.inspect(binary_pair["metadata"][0])
    areas=elf.elf_regions(library)
    raw=script.read_bytes()
    output=source_script_candidates(json.loads(raw),meta,areas,library)
    output["thirdPartyScriptJsonSha256"]=hashlib.sha256(raw).hexdigest()
    dest=root/"output/ref04-p2-native-code-address-candidates.json"
    dest.parent.mkdir(parents=True,exist_ok=True)
    tmp=dest.with_suffix(".tmp")
    tmp.write_text(json.dumps(output,ensure_ascii=False,indent=2)+"\n",encoding="utf-8")
    tmp.replace(dest)
    print(json.dumps({"status":output["classification"],
                      "candidateMethodsForManualReview":output["strictCandidateCount"],
                      "sourceRuntimeFormulaProven":False,
                      "originalUIAssetsChanged":False},sort_keys=True))
    return output

if __name__=="__main__":
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root",type=Path,default=ROOT)
    parser.add_argument("--script-json",type=Path,required=True,
                        help="Third-party IL2CPP dumper script.json in local ignored output")
    args=parser.parse_args()
    execute(args.root.resolve(),args.script_json.resolve())
