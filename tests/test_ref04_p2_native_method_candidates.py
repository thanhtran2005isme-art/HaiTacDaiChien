"""Never promote uncorroborated third-party method pointer candidates to P2 runtime."""
import hashlib
import importlib
from pathlib import Path
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
sys.path.insert(0,str(ROOT/"tests"))
p=importlib.import_module("ref04_p2_native_method_candidates")
elf=importlib.import_module("ref04_arm64_elf_regions")
fixture=importlib.import_module("test_ref04_arm64_elf_regions").fixture

def input_files():
    blob=fixture()
    idx={
        "classification":"REF04_IL2CPP_V31_SOURCE_METHOD_INDEX_NO_CODE_MAPPING",
        "metadataSha256":"f"*64,
        "targetClassDefinitions":[{
            "className":"SafeAreaAdapter","namespace":"",
            "methods":[{"name":"ApplySafeArea","methodDefinitionIndex":44,
                        "methodToken":"0x0600002d"}]}],
    }
    script={"ScriptMethod":[{"Name":"SafeAreaAdapter$$ApplySafeArea",
                             "Address":0x1110}]}
    return script,idx,elf.elf_regions(blob),blob

class CandidateOnly(unittest.TestCase):
    def test_original_executable_address_is_only_a_candidate(self):
        data,idx,exe,blob=input_files()
        result=p.source_script_candidates(data,idx,exe,blob)
        self.assertEqual(result["strictCandidateCount"],1)
        method=result["candidates"][0]
        self.assertEqual(method["originalELFFileOffset"],0x110)
        self.assertEqual(method["sourceMethodToken"],"0x0600002d")
        self.assertEqual(method["first16ExecutableBytesSha256"],
                         hashlib.sha256(blob[0x110:0x120]).hexdigest())
        self.assertFalse(method["methodOwnershipIndependentlyProven"])
        self.assertFalse(method["runtimeExpressionProven"])
        self.assertFalse(result["runtimeFormulaRecovered"])
        self.assertFalse(result["sourceFieldApplicationAllowed"])

    def test_duplicate_overload_or_name_blocks_address_assignment(self):
        data,idx,exe,blob=input_files()
        idx["targetClassDefinitions"][0]["methods"].append(
            {"name":"ApplySafeArea","methodDefinitionIndex":45,
             "methodToken":"0x0600002e"})
        result=p.source_script_candidates(data,idx,exe,blob)
        self.assertEqual(result["strictCandidateCount"],0)
        self.assertEqual(result["unresolvedCountByReason"][
            "NOT_UNIQUE_METHOD_DEFINITION_OR_SCRIPT_MATCH"],1)

    def test_out_of_range_fake_address_cannot_become_native_method(self):
        data,idx,exe,blob=input_files()
        data["ScriptMethod"][0]["Address"]=0x06000001
        result=p.source_script_candidates(data,idx,exe,blob)
        self.assertEqual(result["strictCandidateCount"],0)
        self.assertFalse(result["nativeCodeMethodOwnershipVerified"])

    def test_wrong_source_elf_sha_is_rejected(self):
        data,idx,exe,blob=input_files()
        exe["originalLibrarySha256"]="0"*64
        with self.assertRaisesRegex(ValueError,"source dumper"):
            p.source_script_candidates(data,idx,exe,blob)

if __name__=="__main__":
    unittest.main()
