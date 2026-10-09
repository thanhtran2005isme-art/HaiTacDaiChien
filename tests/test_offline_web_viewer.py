"""Local-only offline web UI viewer: HTTP, privacy and metadata smoke tests."""
from __future__ import annotations

import importlib.util
import json
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

    def test_js_css_available_without_external_network(self):
        for uri, fragment in [
            ("/app.js", b"bootstrap()"),
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
