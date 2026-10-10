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

def decode_itanium_nested_symbol(symbol):
    """Strict Itanium _ZN length-prefix parser; no fuzzy name matching.

    Returns (qualified class string, method string) only for plain nested
    names. Templates, substitutions, operators and malformed lengths are
    intentionally blocked; a decoded symbol is still a *candidate* only.
    """
    if not isinstance(symbol,str) or not symbol.startswith("_ZN"):
        return None
    position=3
    parts=[]
    while position < len(symbol) and symbol[position] != "E":
        if len(parts) >= 20 or not symbol[position].isdigit():
            return None
        start=position
        while position < len(symbol) and symbol[position].isdigit():
            position+=1
            if position-start>4:
                return None
        digits=symbol[start:position]
        if len(digits)>1 and digits[0]=="0":
            return None
        count=int(digits)
        if not 0<count<=256 or position+count>len(symbol):
            return None
        member=symbol[position:position+count]
        if any(not (ch.isalnum() or ch in "_.$") for ch in member):
            return None
        parts.append(member)
        position+=count
    if position>=len(symbol) or symbol[position]!="E" or len(parts)<2:
        return None
    # Remaining bytes carry parameter ABI types; not used as method names.
    if len(symbol)-position>256:
        return None
    return (".".join(parts[:-1]),parts[-1])

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
    by_itanium=collections.defaultdict(list)
    rows=doc["ScriptMethod"]
    if len(rows)>2_000_000:
        raise ValueError("Untrusted native script output method count")
    output_stats=collections.Counter()
    for row in rows:
        name=row.get("Name") if isinstance(row,dict) else None
        address=row.get("Address") if isinstance(row,dict) else None
        output_stats["scriptEntries"]+=1
        if isinstance(name,str) and ("PanelHome2" in name or "SafeAreaAdapter" in name):
            output_stats["scriptNamesMentionOriginalTargetClass"]+=1
        if isinstance(address,str):
            if (len(address)<=18 and address.startswith(("0x","0X"))
                and all(x in "0123456789abcdefABCDEF" for x in address[2:])):
                address=int(address,16)
                output_stats["sourceHexStringAddressFields"]+=1
            else:
                output_stats["unsupportedAddressEncoding"]+=1
                continue
        if not isinstance(name,str) or not isinstance(address,int) or (
            address<=0 or address>=(1<<63) or len(name)>256):
            output_stats["unusableScriptEntries"]+=1
            continue
        by_name[name].append(address)
        decoded=decode_itanium_nested_symbol(name)
        if decoded is not None:
            by_itanium[decoded].append(address)
            output_stats["itaniumNestedSymbolsParsed"]+=1
    candidates=[]
    blockers=collections.Counter()
    # Diagnose known dumper name conventions without logging game symbols.
    target_names={cls+"$$"+meth for cls,meth in source_methods}
    output_stats["exactNameMatchesInScript"]=sum(
        len(by_name.get(label,[])) for label in target_names)
    output_stats["directClassMethodNameMatches"]=sum(
        len(by_name.get(cls+method,[])) for cls,method in source_methods)
    source_classes={cls for cls,_ in source_methods}
    class_method_pairs=set(source_methods)
    scoped_names=[name for name in by_name if any(
        cls in name for cls in source_classes)]
    output_stats["scriptNamesContainingSourceClass"]=len(scoped_names)
    separators=collections.Counter()
    for name in scoped_names:
        for cls,method in class_method_pairs:
            if not name.endswith(method):
                continue
            prefix=name[:-len(method)]
            if prefix == cls:
                separators["EXACT_CLASS_METHOD_NO_DELIMITER"]+=1
            elif prefix.endswith(cls+"$$"):
                separators["CLASS_DOUBLE_DOLLAR"]+=1
            elif prefix.endswith(cls+"__"):
                separators["CLASS_DOUBLE_UNDERSCORE"]+=1
            elif prefix.endswith(cls+"_"):
                separators["CLASS_SINGLE_UNDERSCORE"]+=1
            elif prefix.endswith(cls+"::"):
                separators["CLASS_SCOPE"]+=1
            elif prefix.endswith(cls+"."):
                separators["CLASS_DOT"]+=1
            else:
                separators["SOURCE_CLASS_PRESENT_OTHER_FORMAT"]+=1
    output_stats.update({"sourceClassMethodSuffix_"+key:num
                         for key,num in sorted(separators.items())})
    # Read-only aggregate diagnostics: never publish IL2CPP symbol names,
    # script.json names, metadata method names, or source string values.
    patterns=collections.Counter()
    method_matches=collections.Counter()
    for name in scoped_names:
        for cls in source_classes:
            at=name.find(cls)
            if at < 0:
                continue
            tail=name[at+len(cls):]
            if not tail:
                patterns["CLASS_END"]+=1
            elif tail.startswith("$$"):
                patterns["CLASS_THEN_DOUBLE_DOLLAR"]+=1
            elif tail.startswith("::"):
                patterns["CLASS_THEN_SCOPE"]+=1
            elif tail.startswith("__"):
                patterns["CLASS_THEN_DOUBLE_UNDERSCORE"]+=1
            elif tail.startswith("_"):
                patterns["CLASS_THEN_UNDERSCORE"]+=1
            elif tail.startswith("."):
                patterns["CLASS_THEN_DOT"]+=1
            else:
                patterns["CLASS_THEN_OTHER"]+=1
                # Unicode/ASCII codepoint only, never symbol or game text.
                char=tail[0]
                if ord(char)<128 and not char.isalnum():
                    patterns["OTHER_NEXT_ASCII_HEX_"+format(ord(char),"02X")]+=1
                elif char.isalnum():
                    patterns["OTHER_NEXT_ALPHANUMERIC"]+=1
                else:
                    patterns["OTHER_NEXT_NON_ASCII"]+=1
            if cls in {k for k,_ in class_method_pairs}:
                for class_name,method in class_method_pairs:
                    if class_name != cls or not method:
                        continue
                    if method in tail:
                        method_matches["TARGET_METHOD_NAME_APPEARS_AFTER_CLASS"]+=1
                    if (len(tail)>=len(method)+2 and method in tail and
                        any(tail.startswith(delim+method) for delim in
                            ("$$","::","__","_","."))):
                        method_matches["TARGET_METHOD_PREFIX_AFTER_CLASS"]+=1
    output_stats.update({"scriptClassShape_"+key:value
                         for key,value in sorted(patterns.items())})
    output_stats.update({"sourceMethodPattern_"+key:value
                         for key,value in sorted(method_matches.items())})
    for (cls,method), definitions in sorted(source_methods.items()):
        # Some original IL2CPP symbol dumpers concatenate class/method names
        # without a delimiter. Accept only exact, unambiguous equality; never
        # fuzzy-match a method substring to an arbitrary native address.
        labels=(cls+"$"+method,cls+method)
        hits=[(label,addr) for label in labels for addr in by_name.get(label,[])]
        hits.extend(("ITANIUM_ABI_NESTED",addr)
                    for addr in by_itanium.get((cls,method),[]))
        if len(definitions)!=1 or len(hits)!=1:
            blockers["NOT_UNIQUE_METHOD_DEFINITION_OR_SCRIPT_MATCH"]+=1
            continue
        matching_label,addr=hits[0]
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
            "nameMatchRule":("EXACT_DOUBLE_DOLLAR" if matching_label==labels[0]
                             else "EXACT_CLASS_METHOD_CONCATENATION"
                             if matching_label==labels[1] else "ITANIUM_ABI_NESTED"),
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
        "sanitizedScriptFormatCounts":dict(sorted(output_stats.items())),
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
