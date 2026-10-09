"""Synthetic-only safety tests: do not require proprietary game binaries."""
import io
import pathlib
import struct
import sys
import tempfile
import types
import unittest
import zipfile

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
import recover_managed_ui_fields as binary
import probe_xapk_typetree_backends as probe


def pointer(pid, file_id=0):
    return {"m_FileID": file_id, "m_PathID": pid}


class Root:
    def __init__(self, *, root_type="MonoBehaviour", level=0,
                 fields=("m_GameObject", "m_Script", "m_Enabled", "m_Type")):
        self.m_Type = root_type
        self.m_Name = "Base"
        self.m_Level = level
        self.m_Children = [types.SimpleNamespace(m_Name=name) for name in fields]


class StrictReader:
    def __init__(self, result=None):
        self.tree = result or {
            "m_GameObject": pointer(42), "m_Script": pointer(77, 1),
            "m_Enabled": False, "m_Type": 2, "m_PreserveAspect": True}
        self.strict = False
    def get_raw_data(self):
        return b"source-serialized-object"
    def read_typetree(self, nodes, check_read=False):
        assert nodes.m_Type in ("MonoBehaviour", "Image")
        self.strict = check_read
        return self.tree


class Generator:
    def get_nodes_up(self, assembly, cls):
        assert assembly == "UnityEngine.UI"
        assert cls == "UnityEngine.UI.Image"
        return Root()


ROW = {
    "kind": "MonoBehaviour", "className": "UnityEngine.UI.Image",
    "assembly": "UnityEngine.UI", "scriptResolution": "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID",
    "gameObjectId": 42, "scriptPointer": {"fileId": 1, "pathId": 77},
    "nativeEnabled": False
}


def make_xapk(path, *, duplicate=False, elf=True, version=31):
    def apk(name, data):
        b = io.BytesIO()
        with zipfile.ZipFile(b, "w") as arc:
            arc.writestr(name, data)
        return b.getvalue()
    metadata = struct.pack("<II", binary.refs.IL2CPP_MAGIC, version) + b"\x00" * 256
    with zipfile.ZipFile(path, "w") as outer:
        outer.writestr("base.apk", apk(
            "assets/bin/Data/Managed/Metadata/global-metadata.dat", metadata))
        outer.writestr("config.arm64.apk", apk(
            "lib/arm64-v8a/libil2cpp.so", b"\x7fELFbinary" if elf else b"notanelf"))
        if duplicate:
            outer.writestr("other.apk", apk(
                "lib/x86/libil2cpp.so", b"\x7fELFduplicate"))


class BinaryProofTests(unittest.TestCase):
    def test_backend_probe_selects_only_source_proven_monoscripts(self):
        scenes=[{"components": [
            {"className": "UnityEngine.UI.Image",
             "assembly": "UnityEngine.UI",
             "scriptResolution": "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID"},
            {"className": "UnityEngine.UI.CanvasScaler",
             "assembly": "UnityEngine.UI",
             "scriptResolution": "LOCAL_PATHID"},
            {"className": "UnityEngine.UI.Mask",
             "assembly": "UnityEngine.UI",
             "scriptResolution": "GUESSED_CLASS_NAME"},
            {"className": "Other.Type", "assembly": "UnityEngine.UI",
             "scriptResolution": "LOCAL_PATHID"},
        ]}]
        names = probe.select_source_classes(scenes)
        self.assertEqual([(asm, cls) for asm,cls,_ in names],[
            ("UnityEngine.UI", "UnityEngine.UI.Image"),
            ("UnityEngine.UI", "UnityEngine.UI.CanvasScaler")
        ])

    def test_source_generator_explicit_backend_is_source_bound(self):
        with tempfile.TemporaryDirectory() as folder:
            xapk = pathlib.Path(folder) / "authorized.xapk"
            make_xapk(xapk)
            class Stub:
                def __init__(self, version):
                    self.version = version
                def load_il2cpp(self, lib, meta):
                    assert lib.startswith(b"\x7fELF")
            for backend in ("AssetsTools", "AssetStudio", "AssetRipper"):
                _, proof = binary.source_generator(
                    xapk, "2022.3.51f1", factory=Stub, backend=backend)
                self.assertEqual(proof["backend"], backend)
                self.assertEqual(proof["gameUnityVersion"], "2022.3.51f1")
            with self.assertRaises(binary.RecoveryBlocked):
                binary.source_generator(
                    xapk, "2022.3.51f1", factory=Stub,
                    backend="FAKE_OR_INFERRED")

    def test_source_pair_generator_and_provenance_hashes(self):
        with tempfile.TemporaryDirectory() as folder:
            xapk = pathlib.Path(folder) / "authorized.xapk"
            make_xapk(xapk)
            class Stub:
                def __init__(self, version):
                    self.version = version
                def load_il2cpp(self, lib, meta):
                    assert lib.startswith(b"\x7fELF")
                    assert struct.unpack_from("<I", meta, 4)[0] == 31
            gen, proof = binary.source_generator(
                xapk, "2020.3.48f1", factory=Stub)
            self.assertEqual(gen.version, "2020.3.48f1")
            self.assertEqual(proof["metadata"]["byteLength"], 264)
            self.assertEqual(len(proof["library"]["sha256"]), 64)

    def test_duplicate_lib_rejected_not_arbitrarily_selected(self):
        with tempfile.TemporaryDirectory() as folder:
            xapk = pathlib.Path(folder) / "game.xapk"
            make_xapk(xapk, duplicate=True)
            with self.assertRaisesRegex(binary.RecoveryBlocked, "duplicate"):
                binary.read_source_pair(xapk)

    def test_wrong_il2cpp_version_or_elf_rejected(self):
        with tempfile.TemporaryDirectory() as folder:
            xapk = pathlib.Path(folder) / "game.xapk"
            make_xapk(xapk, version=30)
            with self.assertRaisesRegex(binary.RecoveryBlocked, "v31"):
                binary.read_source_pair(xapk)
            make_xapk(xapk, elf=False)
            with self.assertRaisesRegex(binary.RecoveryBlocked, "ELF"):
                binary.read_source_pair(xapk)

    def test_only_exact_serialized_unity_version(self):
        self.assertEqual(binary.exact_unity_version(
            [{"unityVersion": "2020.3.48f1\n2"}]), "2020.3.48f1")
        with self.assertRaises(binary.RecoveryBlocked):
            binary.exact_unity_version([{"unityVersion": "0.0.0\n2"}])
        with self.assertRaises(binary.RecoveryBlocked):
            binary.exact_unity_version([
                {"unityVersion": "2020.3.1f1"}, {"unityVersion": "2022.3.21f1"}])

    def test_generated_typetree_checked_for_full_object_and_source(self):
        reader = StrictReader()
        fields, proof = binary.verified_fields(reader, ROW, Generator())
        self.assertEqual(fields, {"m_Type": 2, "m_PreserveAspect": True})
        self.assertTrue(reader.strict)
        self.assertTrue(proof["exactSourcePointerChecked"])
        self.assertEqual(proof["method"], "SOURCE_IL2CPP_GENERATED_TYPETREE")

    def test_native_unitypy_node_reconstruction_without_copying_extension(self):
        try:
            from UnityPy.helpers.TypeTreeNode import TypeTreeNode
        except ImportError:
            self.skipTest("UnityPy optional")
        derived = TypeTreeNode(0, "Image", "Base", 0, 0)
        native = TypeTreeNode(0, "MonoBehaviour", "Base", 0, 0)
        for name in ("m_GameObject", "m_Enabled", "m_Script", "m_Name", "m_Type"):
            derived.m_Children.append(TypeTreeNode(1, "int", name, 0, 0))
        for name in ("m_GameObject", "m_Enabled", "m_Script", "m_Name"):
            native.m_Children.append(TypeTreeNode(1, "int", name, 0, 0))
        merged = binary.verified_native_header_root(derived, native)
        self.assertIsNot(merged, derived)
        self.assertEqual(merged.m_Type, "Image")
        self.assertEqual([x.m_Name for x in merged.m_Children],
                         ["m_GameObject", "m_Enabled", "m_Script", "m_Name", "m_Type"])
        self.assertEqual(len(derived.m_Children), 5)

    def test_native_header_recombination_uses_version_source_nodes_only(self):
        derived = Root(root_type="Image", fields=(
            "m_GameObject", "m_Enabled", "m_Script", "m_Name",
            "m_Type", "m_PreserveAspect"))
        native = Root(fields=(
            "m_ObjectHideFlags", "m_GameObject", "m_Enabled", "m_Script", "m_Name"))
        merged = binary.verified_native_header_root(derived, native)
        self.assertEqual([x.m_Name for x in merged.m_Children], [
            "m_ObjectHideFlags", "m_GameObject", "m_Enabled", "m_Script", "m_Name",
            "m_Type", "m_PreserveAspect"])
        self.assertEqual([x.m_Name for x in derived.m_Children][0],
                         "m_GameObject", "Input generator must not be mutated")

    def test_source_native_header_path_keeps_strict_guards(self):
        class DerivedGenerator:
            def get_nodes_up(self, assembly, cls):
                return Root(root_type="Image", fields=(
                    "m_GameObject", "m_Enabled", "m_Script", "m_Name",
                    "m_Type", "m_PreserveAspect"))
        reader = StrictReader()
        fields, proof = binary.verified_fields(
            reader, ROW, DerivedGenerator(), use_unitypy_native_header=True,
            native_root=Root(fields=(
                "m_GameObject", "m_Enabled", "m_Script", "m_Name")))
        self.assertTrue(reader.strict)
        self.assertEqual(fields["m_Type"], 2)
        self.assertEqual(proof["nativeHeaderMethod"],
                         "UNITYPY_EXACT_SOURCE_UNITY_VERSION")
        reader.tree["m_Script"] = pointer(555, 1)
        with self.assertRaises(binary.RecoveryBlocked) as failure:
            binary.verified_fields(
                reader, ROW, DerivedGenerator(), use_unitypy_native_header=True,
                native_root=Root(fields=(
                    "m_GameObject", "m_Enabled", "m_Script", "m_Name")))
        self.assertEqual(failure.exception.code, "MONOSCRIPT_POINTER_MISMATCH")

    def test_reject_missing_or_ambiguous_native_header_nodes(self):
        derived = Root(root_type="Image", fields=(
            "m_GameObject", "m_Script", "m_Enabled", "m_Type"))
        with self.assertRaises(binary.RecoveryBlocked):
            binary.verified_native_header_root(
                derived, Root(fields=("m_GameObject", "m_Script", "m_Type")))
        with self.assertRaises(binary.RecoveryBlocked):
            binary.verified_native_header_root(
                derived, Root(fields=("m_GameObject", "m_Script", "m_Enabled",
                                       "m_Enabled")))

    def test_derived_image_root_with_exact_native_header_is_valid(self):
        class DerivedGenerator:
            def get_nodes_up(self, assembly, cls):
                return Root(root_type="Image")
        reader = StrictReader()
        fields, proof = binary.verified_fields(reader, ROW, DerivedGenerator())
        self.assertEqual(fields["m_Type"], 2)
        self.assertTrue(reader.strict)
        self.assertEqual(proof["method"], "SOURCE_IL2CPP_GENERATED_TYPETREE")

    def test_stage_specific_generation_assertion_does_not_claim_decode(self):
        class BrokenGenerator:
            def get_nodes_up(self, assembly, cls):
                assert False, "bad generated source node"
        with self.assertRaises(binary.RecoveryBlocked) as failure:
            binary.verified_fields(StrictReader(), ROW, BrokenGenerator())
        self.assertEqual(failure.exception.phase, "generate_nodes")
        self.assertEqual(failure.exception.code, "NODE_GENERATION_AssertionError")
        self.assertIn("get_nodes_up", failure.exception.frame)

    def test_reject_incomplete_monobehaviour_root_before_parsing(self):
        class BrokenGenerator:
            def get_nodes_up(self, assembly, cls):
                return Root(root_type="Behaviour", level=1,
                            fields=("m_Name", "m_Type"))
        reader = StrictReader()
        reader.read_typetree = lambda *a, **kw: self.fail(
            "Cannot parse without source GameObject/MonoScript header")
        with self.assertRaises(binary.RecoveryBlocked) as failure:
            binary.verified_fields(reader, ROW, BrokenGenerator())
        self.assertEqual(failure.exception.phase, "validate_root")
        self.assertEqual(failure.exception.code, "INCOMPLETE_GENERATED_ROOT")
        self.assertEqual(failure.exception.tree["rootLevel"], 1)

    def test_strict_parser_assertion_remains_blocked(self):
        class BrokenReader(StrictReader):
            def read_typetree(self, nodes, check_read=False):
                self.strict = check_read
                assert False, "failed to consume source bytes"
        reader = BrokenReader()
        with self.assertRaises(binary.RecoveryBlocked) as failure:
            binary.verified_fields(reader, ROW, Generator())
        self.assertTrue(reader.strict)
        self.assertEqual(failure.exception.phase, "strict_parse")
        self.assertEqual(failure.exception.code, "STRICT_PARSE_AssertionError")

    def test_reject_mismatch_to_gameobject_script_or_enabled(self):
        for key, value in (
            ("m_GameObject", pointer(999)),
            ("m_Script", pointer(77, 0)),
            ("m_Enabled", True),
        ):
            reader = StrictReader()
            reader.tree[key] = value
            with self.subTest(key=key), self.assertRaises(binary.RecoveryBlocked) as err:
                binary.verified_fields(reader, ROW, Generator())
            self.assertEqual(err.exception.phase, "source_compare")
            self.assertEqual(err.exception.code, {
                "m_GameObject": "GAMEOBJECT_POINTER_MISMATCH",
                "m_Script": "MONOSCRIPT_POINTER_MISMATCH",
                "m_Enabled": "ENABLED_VALUE_MISMATCH",
            }[key])

    def test_invalid_enum_and_synthetic_fallback_rejected(self):
        reader = StrictReader()
        reader.tree["m_Type"] = 999
        with self.assertRaisesRegex(binary.RecoveryBlocked, "Invalid"):
            binary.verified_fields(reader, ROW, Generator())
        reader = StrictReader()
        reader.tree.pop("m_Type")
        reader.tree.pop("m_PreserveAspect")
        with self.assertRaisesRegex(binary.RecoveryBlocked, "No target"):
            binary.verified_fields(reader, ROW, Generator())

    def test_unverified_class_and_missing_bytes_rejected(self):
        row = dict(ROW, scriptResolution="GUESSED_CLASS_NAME")
        with self.assertRaises(binary.RecoveryBlocked):
            binary.verified_fields(StrictReader(), row, Generator())
        reader = StrictReader()
        reader.get_raw_data = lambda: b""
        with self.assertRaises(binary.RecoveryBlocked):
            binary.verified_fields(reader, ROW, Generator())


if __name__ == "__main__":
    unittest.main()
