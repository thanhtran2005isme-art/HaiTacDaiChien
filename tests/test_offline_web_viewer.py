"""Local-only offline web UI viewer: HTTP, privacy and metadata smoke tests."""
from __future__ import annotations

import importlib.util
import json
import tempfile
from unittest.mock import patch
import pathlib
import threading
import unittest
from urllib.error import HTTPError
from urllib.request import urlopen

ROOT = pathlib.Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "serve_ui_viewer", ROOT / "tools/serve_ui_viewer.py")
serve = importlib.util.module_from_spec(spec)
spec.loader.exec_module(serve)


class TestOfflineWebViewer(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.server = serve.ThreadingHTTPServer(("127.0.0.1", 0), serve.ViewerHandler)
        cls.thread = threading.Thread(target=cls.server.serve_forever, daemon=True)
        cls.thread.start()
        cls.url = "http://127.0.0.1:" + str(cls.server.server_port)

    @classmethod
    def tearDownClass(cls):
        cls.server.shutdown()
        cls.server.server_close()
        cls.thread.join(timeout=2)

    def request(self, path):
        with urlopen(self.url + path, timeout=5) as response:
            return response.status, response.headers, response.read()

    def test_http_starts_from_browser_root(self):
        status, headers, content = self.request("/")
        self.assertEqual(status, 200)
        self.assertIn(b"Offline UI Viewer", content)
        self.assertIn("text/html", headers.get("Content-Type", ""))
        self.assertEqual(headers.get("Cache-Control"), "no-store")
        self.assertIn("blob:", headers.get("Content-Security-Policy", ""))

    def test_js_css_available_without_external_network(self):
        for uri, fragment in [
            ("/app.js", b"bootstrap()"),
            ("/asset-matching.js", b"buildIndex"),
            ("/style.css", b".wire-node"),
        ]:
            status, _, content = self.request(uri)
            self.assertEqual(status, 200)
            self.assertIn(fragment, content)

    def test_real_generated_scene_data_is_valid(self):
        status, headers, content = self.request("/ui-scenes.json")
        self.assertEqual(status, 200)
        self.assertIn("application/json", headers.get("Content-Type", ""))
        data = json.loads(content)
        self.assertEqual(data["schemaVersion"], 1)
        self.assertEqual(len(data["scenes"]), 5)
        self.assertEqual(sum(x["nodeCount"] for x in data["scenes"]), 1670)
        self.assertEqual(data["scenes"][1]["unlinkedImageEntries"], 46)
        for scene in data["scenes"]:
            self.assertEqual(len(scene["nodes"]), scene["nodeCount"])
            self.assertTrue(scene["nodes"][0]["id"] == scene["rootTransform"])

    def test_private_asset_directories_are_not_served(self):
        for path in ["/private-ui-art/secret.png", "/web-ui-viewer/local-art/a.png",
                     "/assets/portrait.png", "/asset-file?name=a.png"]:
            with self.subTest(path=path):
                with self.assertRaises(HTTPError) as ctx:
                    self.request(path)
                self.assertEqual(ctx.exception.code, 404)

    def test_local_art_status_no_private_paths(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(serve, "ART_ROOT", pathlib.Path(temp)):
            status, _, body = self.request("/local-art/status.json")
            state = json.loads(body)
            self.assertEqual(status, 200)
            self.assertEqual(state["status"], "missing")
            self.assertNotIn("path", state)
            (pathlib.Path(temp) / "export-status.json").write_text(
                json.dumps({"status": "BLOCKED", "reason": "D:/private/game.xapk"}),
                encoding="utf-8")
            _, _, body = self.request("/local-art/status.json")
            self.assertEqual(json.loads(body)["status"], "export_failed")
            self.assertNotIn(b"private", body)
            image = "f" * 32 + ".png"
            (pathlib.Path(temp) / image).write_bytes(b"test-image")
            (pathlib.Path(temp) / "manifest.json").write_text(json.dumps({
                "version": 1, "files": [image],
                "stats": {"sprite_images_exported": 1},
                "scenes": {"REF01": {"/Canvas/Image": image}},
            }), encoding="utf-8")
            _, _, body = self.request("/local-art/status.json")
            self.assertEqual(json.loads(body)["status"], "ready")
            self.assertEqual(json.loads(body)["mapped_nodes"], 1)
            self.assertNotIn(image.encode("utf-8"), body)
            (pathlib.Path(temp) / image).unlink()
            _, _, body = self.request("/local-art/status.json")
            self.assertEqual(json.loads(body)["status"], "missing_png")

    def test_generated_art_only_from_manifest(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(serve, "ART_ROOT", pathlib.Path(temp)):
            allowed = "a" * 32 + ".png"
            other = "b" * 32 + ".png"
            (pathlib.Path(temp) / allowed).write_bytes(b"valid-dummy-art")
            (pathlib.Path(temp) / other).write_bytes(b"never-serve")
            (pathlib.Path(temp) / "manifest.json").write_text(json.dumps({
                "version": 1, "files": [allowed],
                "scenes": {"REF01": {"/Canvas/Bg": allowed}}
            }), encoding="utf-8")
            status, headers, body = self.request("/local-art/" + allowed)
            self.assertEqual(status, 200)
            self.assertEqual(body, b"valid-dummy-art")
            self.assertEqual(headers.get("Cross-Origin-Resource-Policy"), "same-origin")
            status, _, body = self.request("/local-art/manifest.json")
            self.assertEqual(status, 200)
            self.assertEqual(json.loads(body)["version"], 1)
            for url in ["/local-art/" + other, "/local-art/../manifest.json",
                        "/output/local-ui-art/" + allowed,
                        "/local-art/private.png", "/local-art/%2e%2e/manifest.json"]:
                with self.subTest(url=url):
                    with self.assertRaises(HTTPError) as ctx:
                        self.request(url)
                    self.assertEqual(ctx.exception.code, 404)

    def test_reject_malformed_generated_art_manifest(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(serve, "ART_ROOT", pathlib.Path(temp)):
            (pathlib.Path(temp) / "manifest.json").write_text(json.dumps({
                "version": 1, "files": ["../secret.png"]
            }), encoding="utf-8")
            with self.assertRaises(HTTPError) as ctx:
                self.request("/local-art/manifest.json")
            self.assertEqual(ctx.exception.code, 404)

    def test_spine_gallery_static_html_and_javascript(self):
        for path, marker in [("/spine-viewer", b"Spine Animation"),
                             ("/spine-viewer.js", b"SpinePlayer"),
                             ("/spine-viewer.css", b".canvas-shell")]:
            status, _, data = self.request(path)
            self.assertEqual(status, 200)
            self.assertIn(marker, data)

    def test_local_spine_manifest_strict_allowlist(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(
                serve, "SPINE_ROOT", pathlib.Path(temp)):
            token = "a" * 24
            folder = pathlib.Path(temp) / token
            folder.mkdir()
            files = [token + "/skeleton.json", token + "/skeleton.atlas", token + "/hero.png"]
            for name in files:
                (pathlib.Path(temp) / name).write_bytes(b"sample-only")
            (folder / "private.png").write_bytes(b"should-not-serve")
            (pathlib.Path(temp) / "manifest.json").write_text(json.dumps({
                "version": 1, "files": files, "packages": [{
                    "id": token, "name": "sample",
                    "skeleton": files[0], "atlas": files[1], "pages": [files[2]]
                }]
            }), encoding="utf-8")
            for name in files:
                status, _, body = self.request("/local-spine/" + name)
                self.assertEqual(status, 200)
                self.assertEqual(body, b"sample-only")
            status, _, body = self.request("/local-spine/manifest.json")
            self.assertEqual(status, 200)
            self.assertEqual(json.loads(body)["version"], 1)
            for path in ["/local-spine/" + token + "/private.png",
                         "/local-spine/" + token + "/../manifest.json",
                         "/local-spine/private.png", "/output/local-spine/" + files[0]]:
                with self.subTest(path=path):
                    with self.assertRaises(HTTPError) as exc:
                        self.request(path)
                    self.assertEqual(exc.exception.code, 404)

    def test_spine_manifest_rejects_traversal(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(
                serve, "SPINE_ROOT", pathlib.Path(temp)):
            (pathlib.Path(temp) / "manifest.json").write_text(json.dumps({
                "version": 1, "files": ["../skeleton.json"]
            }), encoding="utf-8")
            with self.assertRaises(HTTPError) as err:
                self.request("/local-spine/manifest.json")
            self.assertEqual(err.exception.code, 404)

    def test_repo_and_arbitrary_files_not_exposed(self):
        for path in [
            "/.git/config", "/README.md", "/tools/serve_ui_viewer.py",
            "/reports/xapk/ui-hierarchy.csv", "/../README.md",
            "/unity-ui-viewer/Assets/Scripts/OfflineUiViewer.cs",
            "/Huy%E1%BB%81n%2BTho%E1%BA%A1i%2BH%E1%BA%A3i%2BT%E1%BA%B7c_1.0.5_APKPure.xapk"
        ]:
            with self.subTest(path=path):
                with self.assertRaises(HTTPError) as ctx:
                    self.request(path)
                self.assertEqual(ctx.exception.code, 404)

    def test_server_only_binds_loopback(self):
        self.assertEqual(self.server.server_address[0], "127.0.0.1")

    def test_refuses_privileged_port(self):
        with self.assertRaises(ValueError):
            serve.make_server(80)


if __name__ == "__main__":
    unittest.main()
