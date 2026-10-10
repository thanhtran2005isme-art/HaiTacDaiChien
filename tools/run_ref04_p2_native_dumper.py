#!/usr/bin/env python3
"""Optional third-party IL2CPP v31 address discovery in a temporary sandbox.

Passes original XAPK ELF+metadata to a locally installed dumper, but only the
bounded, source-cross-checked candidate report stays in gitignored output.
Never claim runtime UI formulas from dump.cs/script.json method labels.
"""
from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import shutil
import subprocess
import tempfile

import recover_managed_ui_fields as binary
import ref04_p2_native_method_candidates as candidates

ROOT=Path(__file__).resolve().parents[1]

def run(root=ROOT, dumper="il2cpp_dumper"):
    if shutil.which(dumper) is None:
        raise RuntimeError("Native IL2CPP dumper executable unavailable")
    xapk=next(iter(sorted(root.glob("*.xapk"))),None)
    if xapk is None: raise FileNotFoundError("Original XAPK missing")
    pair=binary.read_source_pair(xapk)
    with tempfile.TemporaryDirectory(prefix="ref04_native_source_only_") as folder:
        tmp=Path(folder)
        lib=tmp/"libil2cpp.so"
        meta=tmp/"global-metadata.dat"
        dest=tmp/"private-dump"
        dest.mkdir()
        lib.write_bytes(pair["library"][0])
        meta.write_bytes(pair["metadata"][0])
        proc=subprocess.run(
            [dumper,str(lib),str(meta),str(dest)],
            cwd=tmp,stdin=subprocess.DEVNULL,stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,text=True,timeout=360,check=False)
        # Don't print external tool output: may contain strings from source.
        matches=list(dest.rglob("script.json"))
        if len(matches)!=1:
            return {"classification":"BLOCKED_NO_UNIQUE_NATIVE_SCRIPT_SOURCE",
                    "dumperExitCode":proc.returncode,
                    "nativeMethodCandidatesVerified":0,
                    "runtimeFormulaProven":False}
        report=candidates.execute(root,matches[0])
        return {"classification":report["classification"],
                "dumperExitCode":proc.returncode,
                "sourceAddressCandidatesForManualReview":report["strictCandidateCount"],
                "nativeMethodOwnershipProven":False,
                "runtimeFormulaProven":False}

if __name__=="__main__":
    cli=argparse.ArgumentParser(description=__doc__)
    cli.add_argument("--root",type=Path,default=ROOT)
    cli.add_argument("--dumper",default="il2cpp_dumper")
    args=cli.parse_args()
    try:
        result=run(args.root.resolve(),args.dumper)
    except (RuntimeError,FileNotFoundError,subprocess.TimeoutExpired,ValueError) as exc:
        result={"classification":"BLOCKED_NATIVE_DUMPER_"+type(exc).__name__,
                "sourceAddressCandidatesForManualReview":0,
                "nativeMethodOwnershipProven":False,"runtimeFormulaProven":False}
    print(json.dumps(result,sort_keys=True))
