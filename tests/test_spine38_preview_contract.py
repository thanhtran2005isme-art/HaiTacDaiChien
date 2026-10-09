"""Contract tests for optional Spine-Unity 3.8 opt-in preview.

No runtime or copyrighted art required. These do not replace a real Unity
Editor compile/playback check; they guard against auto-binding guesses and
accidentally publishing Spine runtimes/assets.
"""
from __future__ import annotations
import pathlib
import re
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
EDITOR = ROOT / "unity-ui-viewer/Assets/Editor/OfflineSpine38Preview.cs"
PROBE = ROOT / "unity-ui-viewer/Assets/Scripts/SpinePreviewProbe.cs"
REBUILD = ROOT / "unity-ui-viewer/Assets/Editor/UnityCanvasReconstructor.cs"


class TestSpine38PreviewContract(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.editor = EDITOR.read_text(encoding="utf-8")
        cls.probe = PROBE.read_text(encoding="utf-8")
        cls.rebuilder = REBUILD.read_text(encoding="utf-8")

    def test_runtime_is_optional_and_licensed_only(self):
        self.assertNotIn("using Spine;", self.editor)
        self.assertNotIn("using Spine.Unity;", self.editor)
        self.assertIn('FindRuntimeType("Spine.Unity.SkeletonGraphic")', self.editor)
        self.assertIn('FindRuntimeType("Spine.Unity.SkeletonDataAsset")', self.editor)
        self.assertIn("!runtimeInstalled", self.editor)
        self.assertIn("https://esotericsoftware.com/spine-unity-download",
                      self.editor)
        self.assertIn("OfficiallySupportedUnity", self.editor)
        self.assertIn("experimentalUnityVersion", self.editor)

    def test_source_chain_and_animation_are_checked(self):
        self.assertIn("content_chain_verified_field_unverified", self.editor)
        self.assertIn("sourceSkeletonJson", self.editor)
        self.assertIn("sourceAtlasText", self.editor)
        self.assertIn('serialized.FindProperty("skeletonJSON")', self.editor)
        self.assertIn('serialized.FindProperty("atlasAssets")', self.editor)
        self.assertIn('atlasData.FindProperty("atlasFile")', self.editor)
        self.assertIn('GetMethod("FindAnimation"', self.editor)
        self.assertIn("FindMatchingSkeletonData(note, dataType)", self.editor)
        self.assertIn("selectedAnimation > 0", self.editor)
        self.assertIn("AnimationChoices(note).Contains(chosenAnimation)",
                      self.editor)
        self.assertIn("anim.stringValue = chosenAnimation;", self.editor)
        self.assertNotIn('anim.stringValue = "idle"', self.editor)

    def test_spine_import_stages_atlas_pages_then_atlas_then_skeleton(self):
        # Spine 3.8 opens "Could not automatically set the AtlasAsset"
        # when skeleton.json imports before the corresponding atlas is ready.
        source = self.rebuilder.split(
            "private static Dictionary<string, ImportedSpinePack> "
            "ImportSpineSourcePacks(", 1)[1].split(
            "private static void ApplyOriginalSpriteGeometry(", 1)[0]
        p_pages = source.index("var textures = pack.pages.Select(")
        p_atlas = source.index("string atlasPath = CopySource(pack.atlas, true);")
        p_atlas_import = source.index("AssetDatabase.ImportAsset(atlasPath,")
        p_skeleton = source.index("string skeletonPath = CopySource(pack.skeleton, false);")
        p_skeleton_import = source.index("AssetDatabase.ImportAsset(skeletonPath,")
        self.assertLess(p_pages, p_atlas)
        self.assertLess(p_atlas, p_atlas_import)
        self.assertLess(p_atlas_import, p_skeleton)
        self.assertLess(p_skeleton, p_skeleton_import)
        # The same exact source files are retained; no guessed artwork.
        self.assertIn('string destinationName = atlas ? name + ".txt" : name;', source)
        self.assertIn("textures.Any(tex => tex == null)", source)
        self.assertIn("ForceSynchronousImport", source)

    def test_previews_are_isolated_and_not_source_modifications(self):
        self.assertIn('Root + "/SpinePreviews"', self.editor)
        self.assertIn("EditorSceneManager.NewScene(NewSceneSetup.EmptyScene",
                      self.editor)
        self.assertIn("EditorSceneManager.SaveScene(scene, target)", self.editor)
        self.assertIn("SaveCurrentModifiedScenesIfUserWantsTo", self.editor)
        self.assertIn("NOT ORIGINAL GAME UI", self.editor)
        self.assertNotIn('AssetDatabase.SaveAssets()', self.editor)
        # Rebuilding original static scene still does not add SkeletonGraphic.
        self.assertNotIn("AddComponent<Spine.Unity.SkeletonGraphic>",
                         self.rebuilder)
        self.assertNotIn('AddComponent(graphicType)', self.rebuilder)

    def test_progression_probe_reports_only_real_track_advancing(self):
        self.assertIn("spineGraphic.GetType()", self.probe)
        self.assertIn('GetProperty("AnimationState"', self.probe)
        self.assertIn('GetMethod("GetCurrent"', self.probe)
        self.assertIn('GetProperty("TrackTime"', self.probe)
        self.assertIn("current - firstTrackTime > .10f", self.probe)
        self.assertIn("TRACK_ADVANCING", self.probe)
        self.assertIn("Visually inspect Game View", self.probe)

    def test_editor_component_filename_matching(self):
        for name, code in (("OfflineSpine38Preview", self.editor),
                           ("SpinePreviewProbe", self.probe)):
            self.assertRegex(code, rf"public sealed class {name}\b")
        ignore = (ROOT / ".gitignore").read_text(encoding="utf-8")
        self.assertIn("/unity-ui-viewer/Assets/Spine/", ignore)
        self.assertIn("/unity-ui-viewer/Assets/LocalReconstruction/", ignore)
        self.assertIn("*.unitypackage", ignore)

    def test_source_animation_json_38_not_replaced_by_4x(self):
        source = (ROOT / "tools/export_local_spine.py").read_text(encoding="utf-8")
        self.assertIn('version.startswith("3.8.")', source)
        self.assertIn('"texture_missing_or_ambiguous"', source)
        self.assertIn("NO_COMPLETE_PACK", source)
        self.assertIn("Spine 3.8 source packs only", source)


if __name__ == "__main__":
    unittest.main()
