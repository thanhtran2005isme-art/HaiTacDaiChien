"""Source-only REF04 P2 metadata method ownership; no native formulas guessed."""
import importlib
from pathlib import Path
import struct
import sys
import unittest

ROOT=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(ROOT/"tools"))
idx=importlib.import_module("ref04_il2cpp_method_index_v31")


def sample():
    data=bytearray(0x900)
    struct.pack_into("<II",data,0,idx.MAGIC,idx.VERSION)
    text=b"\0SafeAreaAdapter\0PanelHome2\0ApplySafeArea\0.ctor\0"
    data[0x200:0x200+len(text)]=text
    for at,off,size in (
        (idx.STRINGS,0x200,0x100),
        (idx.TYPES,0x400,2*idx.TYPE_STRIDE),
        (idx.METHODS,0x600,2*idx.METHOD_STRIDE)):
        struct.pack_into("<II",data,at,off,size)
    def si(word):
        return text.index(word.encode("utf-8")+b"\0")
    for i,(name,start,method) in enumerate((
        ("SafeAreaAdapter",0,"ApplySafeArea"),("PanelHome2",1,".ctor"))):
        tb=0x400+i*idx.TYPE_STRIDE
        struct.pack_into("<I",data,tb,si(name))
        struct.pack_into("<I",data,tb+idx.TYPE_METHOD_START,start)
        struct.pack_into("<H",data,tb+idx.TYPE_METHOD_COUNT,1)
        mb=0x600+i*idx.METHOD_STRIDE
        struct.pack_into("<II",data,mb,si(method),i)
        struct.pack_into("<I",data,mb+0x18,0x06000001+i)
    return bytes(data)


class MetadataProof(unittest.TestCase):
    def test_exact_type_and_method_owner_from_metadata_v31(self):
        r=idx.inspect(sample())
        self.assertEqual(r["safeAreaClassDefinitions"],1)
        self.assertEqual(r["panelHome2ClassDefinitions"],1)
        self.assertEqual(r["totalMethodDefinitions"],2)
        a,b=r["targetClassDefinitions"]
        self.assertEqual(a["methods"][0]["name"],"ApplySafeArea")
        self.assertEqual(b["methods"][0]["name"],".ctor")
        self.assertEqual(b["methods"][0]["declaringTypeIndex"],1)
        self.assertEqual(b["methods"][0]["methodToken"],"0x06000002")
        self.assertFalse(r["runtimeFormulaRecovered"])
        self.assertEqual(r["methodCodeAddressesResolved"],0)
        self.assertIsNone(a["methods"][0]["nativeAddress"])

    def test_nonzero_namespace_index_may_point_to_empty_source_string(self):
        blob=bytearray(sample())
        strings=bytes(blob[0x200:0x300])
        at=strings.index(b".ctor") + len(b".ctor")
        self.assertEqual(strings[at],0)
        struct.pack_into("<I",blob,0x400+4,at)
        result=idx.inspect(bytes(blob))
        self.assertEqual(result["safeAreaClassDefinitions"],1)
        self.assertEqual(result["targetClassDefinitions"][0]["namespace"],"")

    def test_wrong_metadata_version_and_broken_method_ownership_block(self):
        blob=bytearray(sample())
        struct.pack_into("<I",blob,4,29)
        with self.assertRaisesRegex(idx.MetadataBlocked,"v31"):
            idx.inspect(bytes(blob))
        blob=bytearray(sample())
        struct.pack_into("<I",blob,0x600+4,55)
        with self.assertRaisesRegex(idx.MetadataBlocked,"declaring type"):
            idx.inspect(bytes(blob))

    def test_broken_definition_bounds_and_untrusted_method_token_block(self):
        blob=bytearray(sample())
        struct.pack_into("<I",blob,0x400+idx.TYPE_METHOD_START,100)
        with self.assertRaisesRegex(idx.MetadataBlocked,"range"):
            idx.inspect(bytes(blob))
        blob=bytearray(sample())
        struct.pack_into("<I",blob,0x600+0x18,0x02000001)
        with self.assertRaisesRegex(idx.MetadataBlocked,"token kind"):
            idx.inspect(bytes(blob))

    def test_original_elf_must_be_aarch64_64bit_little_endian(self):
        blob=bytearray(80)
        blob[0:6]=b"\x7fELF\x02\x01"
        struct.pack_into("<H",blob,18,183)
        result=idx.check_library_elf(bytes(blob))
        self.assertEqual(result["machine"],"AARCH64_ELF64")
        self.assertFalse(result["methodPointerMappingVerified"])
        blob[4]=1
        with self.assertRaisesRegex(idx.MetadataBlocked,"ARM64"):
            idx.check_library_elf(bytes(blob))


if __name__=="__main__":
    unittest.main()
