"""Bounded candidate entrypoint ARM64 census is not runtime formula evidence."""
from pathlib import Path
import hashlib
import importlib
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
sys.path.insert(0,str(ROOT/"tests"))
p=importlib.import_module("ref04_p2_arm64_candidate_census")
elf=importlib.import_module("ref04_arm64_elf_regions")
fixture=importlib.import_module("test_ref04_arm64_elf_regions").fixture

class Ins:
    def __init__(self,address,mnemonic):
        self.address=address
        self.size=4
        self.mnemonic=mnemonic

def decoder(data,addr):
    for i in range(0,len(data)//4):
        yield Ins(addr+i*4,("mov" if i==0 else "ret"))

def source():
    blob=fixture()
    regions=elf.elf_regions(blob)
    addr=0x1110
    c={
        "originalMethodDefinitionIndex":100,
        "sourceMethodToken":"0x06000010",
        "candidateELFVirtualAddress":addr,
        "originalELFFileOffset":0x110,
        "first16ExecutableBytesSha256":hashlib.sha256(blob[0x110:0x120]).hexdigest(),
        "methodOwnershipIndependentlyProven":False,
        "runtimeExpressionProven":False,
        "fullMethodBodyVerified":False,
    }
    inventory={
        "classification":"REF04_P2_NATIVE_METHOD_ADDRESS_CANDIDATES_NEED_REVIEW",
        "sourceELFLibrarySha256":regions["originalLibrarySha256"],
        "strictCandidateCount":1,
        "nativeCodeMethodOwnershipVerified":False,
        "runtimeFormulaRecovered":False,
        "candidates":[c],
    }
    return blob,regions,inventory

class SourceCensus(unittest.TestCase):
    def test_exact_original_arm64_bounds_and_no_formula_claim(self):
        blob,regions,inventory=source()
        result=p.audit(blob,regions,inventory,decoder)
        self.assertEqual(result["inspectedCandidateEntryPoints"],1)
        self.assertIsNone(result["runtimeAlignmentFormula"])
        self.assertFalse(result["runtimeAlignmentProven"])
        entry=result["originalSourceOnlyInstructionCensus"][0]
        self.assertEqual(entry["instructionCount"],2)
        self.assertEqual(entry["inspectedOriginalBytes"],8)
        self.assertTrue(entry["entryTerminatorEncountered"])
        self.assertFalse(entry["completeNativeMethodBodyProven"])

    def test_fake_method_body_claim_or_bad_source_digest_rejected(self):
        blob,regions,inventory=source()
        inventory["candidates"][0]["methodOwnershipIndependentlyProven"]=True
        with self.assertRaisesRegex(ValueError,"falsely promoted"):
            p.audit(blob,regions,inventory,decoder)
        inventory["candidates"][0]["methodOwnershipIndependentlyProven"]=False
        inventory["candidates"][0]["first16ExecutableBytesSha256"]="0"*64
        with self.assertRaisesRegex(ValueError,"bytes SHA"):
            p.audit(blob,regions,inventory,decoder)

    def test_neighbor_cannot_cut_into_unverified_instruction(self):
        blob,regions,inventory=source()
        candidate=inventory["candidates"][0]
        with self.assertRaisesRegex(ValueError,"neighbor"):
            p.inspect_entry(candidate,blob,regions,decoder,0x1110)
        with self.assertRaisesRegex(ValueError,"neighbor"):
            p.inspect_entry(candidate,blob,regions,decoder,0x1111)

    def test_source_binary_pair_mismatch_is_blocked(self):
        blob,regions,inventory=source()
        inventory["sourceELFLibrarySha256"]="f"*64
        with self.assertRaisesRegex(ValueError,"provenance mismatch"):
            p.audit(blob,regions,inventory,decoder)

if __name__=="__main__":
    unittest.main()
