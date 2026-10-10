"""Source-only I2 localizer two-backend equality, no dynamic text guesses."""
import importlib
from pathlib import Path
import sys
import unittest

sys.path.insert(0,str(Path(__file__).resolve().parents[1]/"tools"))
probe=importlib.import_module("probe_ref04_p3_localizer_binary")
OK="STRICT_SOURCE_PARSED_SOURCE_TERM_KEYS_UNVERIFIED_RUNTIME"

class LocalizerBinaryChecks(unittest.TestCase):
    def test_two_independent_type_trees_same_hashed_term(self):
        fields={"mTerm":{"originalUtf8Sha256":"a"*64,"originalByteLength":6}}
        status,payload=probe.compare_two((OK,fields),(OK,dict(fields)))
        self.assertEqual(status,"DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY")
        self.assertEqual(payload,fields)

    def test_missing_second_backend_and_conflicting_term_blocks(self):
        a=(OK,{"mTerm":{"originalUtf8Sha256":"a"*64,"originalByteLength":6}})
        status,payload=probe.compare_two(a,("BLOCKED_SOURCE_TYPETREE_MISSING",{}))
        self.assertTrue(status.startswith("BLOCKED_INDEPENDENT_SOURCE_SCHEMA_"))
        self.assertEqual(payload,{})
        status,payload=probe.compare_two(
            a,(OK,{"mTerm":{"originalUtf8Sha256":"b"*64,"originalByteLength":6}}))
        self.assertEqual(status,"BLOCKED_SOURCE_TERM_HASH_OR_FIELD_CONFLICT")
        self.assertEqual(payload,{})

    def test_zero_recovered_terms_is_not_runtime_translation(self):
        status,payload=probe.compare_two((OK,{}),(OK,{}))
        self.assertEqual(status,"DUAL_BACKEND_LOCALIZER_TERMS_SOURCE_ONLY")
        self.assertEqual(payload,{})
        self.assertFalse(bool(payload))

if __name__=="__main__":
    unittest.main()
