"""Evidence-only tests for native GameObject/MonoScript/IL2CPP/UI field joins."""
import importlib
import pathlib
import struct
import sys
import tempfile
import types
import unittest
import zipfile

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
mod = importlib.import_module("decode_xapk_ui_provenance")


def p(pid, fileid=0):
    return types.SimpleNamespace(m_PathID=pid, m_FileID=fileid)


class Fake:
    def __init__(self, kind, path_id, body, af=None, tree=None):
        self.type = types.SimpleNamespace(name=kind)
        self.path_id = path_id
        self.assets_file = af or types.SimpleNamespace(externals=[])
        self.body = body
        self.tree = tree
    def read(self):
        return self.body
    def parse_monobehaviour_head(self):
        return self.body
    def read_typetree(self):
        if self.tree is None:
            raise TypeError("IL2CPP stripped managed typetree")
        return self.tree


class SourceUIProvenance(unittest.TestCase):
    def setUp(self):
        self.af = types.SimpleNamespace(externals=[])
        self.script = Fake("MonoScript", 31, {
            "m_ClassName": "Image", "m_Namespace": "UnityEngine.UI",
            "m_AssemblyName": "UnityEngine.UI"}, self.af)
        self.behaviour = Fake("MonoBehaviour", 32, {
            "m_GameObject": p(200), "m_Enabled": 0,
            "m_Script": p(31)}, self.af)
        self.ctx = {"readers": {(id(self.af), 31): self.script},
                    "aliases": {}}

    def test_script_name_must_come_from_real_monoscript(self):
        row = mod.inspect_component(self.behaviour, "MonoBehaviour", 200, self.ctx)
        self.assertEqual(row["className"], "UnityEngine.UI.Image")
        self.assertEqual(row["scriptPointer"], {"fileId": 0, "pathId": 31})
        self.assertEqual(row["scriptResolution"], "LOCAL_PATHID")
        self.assertEqual(row["status"], "NO_MANAGED_TYPETREE")
        self.assertIs(row["nativeEnabled"], False)
        self.assertNotIn("fields", row)
        self.assertNotIn("m_Type", row)

    def test_ui_fields_only_when_real_typetree_contains_them(self):
        self.behaviour.tree = {"m_Type": 2, "m_PreserveAspect": True,
                               "privateImageData": {"danger": 111}}
        row = mod.inspect_component(self.behaviour, "MonoBehaviour", 200, self.ctx)
        self.assertEqual(row["status"], "SERIALIZED_FIELDS_VERIFIED")
        self.assertEqual(row["fields"], {
            "m_Type": 2, "m_PreserveAspect": True})
        self.assertNotIn("privateImageData", row["fields"])

    def test_owner_mismatch_fails_closed(self):
        with self.assertRaisesRegex(ValueError, "owner differs"):
            mod.inspect_component(self.behaviour, "MonoBehaviour", 201, self.ctx)

    def test_missing_script_does_not_fallback_to_gameobject_name(self):
        self.behaviour.body["m_Script"] = p(999)
        row = mod.inspect_component(self.behaviour, "MonoBehaviour", 200, self.ctx)
        self.assertEqual(row["status"], "SOURCE_SCRIPT_UNRESOLVED")
        self.assertNotIn("className", row)

    def test_native_canvas_only_reads_real_serialized_fields(self):
        native = Fake("Canvas", 54, {
            "m_GameObject": p(200), "m_Enabled": True,
            "m_RenderMode": 1, "someExtra": 100}, self.af)
        row = mod.inspect_component(native, "Canvas", 200, self.ctx)
        self.assertEqual(row["status"], "NATIVE_FIELDS")
        self.assertEqual(row["fields"], {
            "m_Enabled": True, "m_RenderMode": 1})

    def test_invalid_component_kind_fails(self):
        with self.assertRaisesRegex(ValueError, "Component type differs"):
            mod.inspect_component(self.behaviour, "Canvas", 200, self.ctx)

    def test_cross_bundle_monoscript_resolves_exact_source_alias_and_pathid(self):
        import io
        af = types.SimpleNamespace(name="shared.assets", externals=[])
        src = Fake("MonoScript", 31, {
            "m_ClassName": "CanvasScaler", "m_Namespace": "UnityEngine.UI",
            "m_AssemblyName": "UnityEngine.UI"}, af)
        env = types.SimpleNamespace(objects=[src],
                                    files={"shared.assets": af})
        engine = types.SimpleNamespace(load=lambda blob: env)
        row = {
            "pathId": 35, "kind": "MonoBehaviour",
            "status": "SOURCE_SCRIPT_UNRESOLVED",
            "externalScriptAlias": "shared.assets",
            "scriptPointer": {"fileId": 1, "pathId": 31},
            "graphFieldStatus": "managed_fields_unavailable",
        }
        scene = {"components": [row]}
        with tempfile.TemporaryDirectory() as folder:
            apk_bytes = io.BytesIO()
            with zipfile.ZipFile(apk_bytes, "w") as apk:
                apk.writestr("assets/ui.unity3d", b"mock bundle")
            xapk_file = pathlib.Path(folder) / "game.xapk"
            with zipfile.ZipFile(xapk_file, "w") as xapk:
                xapk.writestr("base.apk", apk_bytes.getvalue())
            audit = mod.resolve_cross_bundle_scripts(xapk_file, [scene], engine)
        self.assertEqual(audit["resolved"], 1)
        self.assertEqual(row["className"], "UnityEngine.UI.CanvasScaler")
        self.assertEqual(row["status"], "NO_MANAGED_TYPETREE")
        self.assertEqual(row["scriptResolution"],
                         "GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")
        self.assertNotIn("fields", row)

    def test_metadata_is_read_from_canonical_xapk_when_unpacked_apks_missing(self):
        import io
        with tempfile.TemporaryDirectory() as folder:
            root = pathlib.Path(folder)
            nested = io.BytesIO()
            with zipfile.ZipFile(nested, "w") as apk:
                apk.writestr(
                    "assets/bin/Data/Managed/Metadata/global-metadata.dat",
                    struct.pack("<II", mod.refs.IL2CPP_MAGIC, 31) +
                    b"\x00" * 256 + b"m_Type\x00")
            with zipfile.ZipFile(root / "game.xapk", "w") as xapk:
                xapk.writestr("base.apk", nested.getvalue())
            result = mod.inspect_il2cpp_header(root / "output/apks",
                                               root / "game.xapk")
            self.assertEqual(len(result["files"]), 1)
            self.assertEqual(result["files"][0]["version"], 31)
            self.assertTrue(result["files"][0]["fieldNameHints"]["m_Type"])

    def test_il2cpp_v31_string_presence_is_only_a_hint(self):
        with tempfile.TemporaryDirectory() as temp:
            path = pathlib.Path(temp) / "source.apk"
            payload = struct.pack("<II", mod.refs.IL2CPP_MAGIC, 31)
            payload += b"\x00" * 260 + b"m_Type\x00m_ReferenceResolution\x00"
            with zipfile.ZipFile(path, "w") as z:
                z.writestr(
                    "assets/bin/Data/Managed/Metadata/global-metadata.dat", payload)
            result = mod.inspect_il2cpp_header(path.parent)
        self.assertEqual(len(result["files"]), 1)
        entry = result["files"][0]
        self.assertEqual(entry["status"], "V31_HEADER_VALIDATED_NAME_HINTS_ONLY")
        self.assertTrue(entry["fieldNameHints"]["m_Type"])
        self.assertFalse(entry["fieldNameHints"]["m_Color"])
        self.assertIn("do not establish", result["limitation"])


if __name__ == "__main__":
    unittest.main()
