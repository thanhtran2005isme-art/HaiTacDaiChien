"""Fail-closed ELF64/AArch64 segment identity tests for original IL2CPP."""
import importlib
import pathlib
import struct
import sys
import unittest

sys.path.insert(0,str(pathlib.Path(__file__).resolve().parents[1]/"tools"))
elf=importlib.import_module("ref04_arm64_elf_regions")

def fixture():
    buf=bytearray(1024)
    buf[:6]=b"\x7fELF\x02\x01"
    buf[6]=1
    struct.pack_into("<H",buf,18,183)
    struct.pack_into("<Q",buf,32,64)
    struct.pack_into("<HHH",buf,52,64,56,1)
    # PT_LOAD executable, on-disk at 0x100, ELF VA at 0x1000
    struct.pack_into("<IIQQQQQQ",buf,64,1,5,0x100,0x1100,0,0x40,0x80,0x100)
    buf[0x100:0x140]=b"\x1f\x20\x03\xd5"*16
    return bytes(buf)

class Arm64ElfProvenance(unittest.TestCase):
    def test_executable_file_ranges_not_method_addresses(self):
        result=elf.elf_regions(fixture())
        self.assertEqual(result["executableLoadSegments"],1)
        self.assertEqual(elf.checked_original_offset(result,0x1110),0x110)
        self.assertFalse(result["methodTokenToNativeAddressProven"])
        self.assertFalse(result["runtimeAlignmentFormulaProven"])
        self.assertFalse(result["unityImportAllowed"])

    def test_outside_file_backed_segment_and_ambiguous_inputs_block(self):
        result=elf.elf_regions(fixture())
        for addr in (0x1150,0x1000,-3,0x06000001):
            with self.assertRaises(elf.ElfBlocked):
                elf.checked_original_offset(result,addr)

    def test_truncated_elf_and_non_aarch64_rejected(self):
        with self.assertRaises(elf.ElfBlocked):
            elf.elf_regions(b"\x7fELF")
        bad=bytearray(fixture())
        struct.pack_into("<H",bad,18,62)
        with self.assertRaisesRegex(elf.ElfBlocked,"AArch64"):
            elf.elf_regions(bytes(bad))

    def test_executable_segment_file_out_of_bounds_rejected(self):
        bad=bytearray(fixture())
        struct.pack_into("<Q",bad,64+8+32,9999)
        with self.assertRaises(elf.ElfBlocked):
            elf.elf_regions(bytes(bad))

if __name__=="__main__":
    unittest.main()
