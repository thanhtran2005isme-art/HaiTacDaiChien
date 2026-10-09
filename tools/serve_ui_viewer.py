#!/usr/bin/env python3
"""Local-only offline UI viewer with a strict allowlist of public metadata files.

No network download, authentication, game server, APK or Unity installation.
Serves only allowlisted UI metadata and locally generated Sprite PNGs over 127.0.0.1.
"""
from __future__ import annotations

import argparse
import json
import re
from functools import partial
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
import threading
import webbrowser

ROOT = Path(__file__).resolve().parents[1]
# Generated images remain ignored by Git. Only manifest-enumerated PNGs are exposed.
ART_ROOT = ROOT / "output/local-ui-art"
ART_NAME = re.compile(r"[0-9a-f]{32}\.png")


def local_art_resource(pathname):
    """Opt-in generated-art bridge: never serve arbitrary local files."""
    if not pathname.startswith("/local-art/"):
        return None
    manifest_path = ART_ROOT / "manifest.json"
    if not manifest_path.is_file() or manifest_path.is_symlink():
        return None
    try:
        manifest = json.loads(manifest_path.read_text(encoding="utf-8"))
        if manifest.get("version") != 1 or not isinstance(manifest.get("files"), list):
            return None
        names = manifest["files"]
        if len(names) > 2000 or any(
            not isinstance(name, str) or not ART_NAME.fullmatch(name)
            for name in names
        ):
            return None
    except (OSError, ValueError, UnicodeError):
        return None
    if pathname == "/local-art/manifest.json":
        return (manifest_path, "application/json; charset=utf-8")
    name = pathname.removeprefix("/local-art/")
    if not ART_NAME.fullmatch(name) or name not in names:
        return None
    image = ART_ROOT / name
    if not image.is_file() or image.is_symlink():
        return None
    return (image, "image/png")

def local_art_status():
    """Public diagnostic counts only; never expose private local filenames or paths."""
    result = {"version": 1, "status": "missing", "sprite_count": 0,
              "mapped_nodes": 0, "file_count": 0}
    manifest_path = ART_ROOT / "manifest.json"
    if not manifest_path.is_file() or manifest_path.is_symlink():
        status_path = ART_ROOT / "export-status.json"
        if status_path.is_file() and not status_path.is_symlink():
            try:
                state = json.loads(status_path.read_text(encoding="utf-8"))
                if state.get("status") in ("BLOCKED", "NO_DECODED_ART"):
                    result["status"] = "export_failed"
            except (OSError, UnicodeError, ValueError):
                pass
        return result
    try:
        info = json.loads(manifest_path.read_text(encoding="utf-8"))
        files = info.get("files")
        if info.get("version") != 1 or not isinstance(files, list) or (
            not files or len(files) > 2000
        ) or any(not isinstance(name, str) or not ART_NAME.fullmatch(name)
                 for name in files):
            result["status"] = "invalid_manifest"
            return result
        if any(not (ART_ROOT / name).is_file() or (ART_ROOT / name).is_symlink()
               for name in files):
            result["status"] = "missing_png"
            return result
        scenes = info.get("scenes")
        stats = info.get("stats", {})
        if not isinstance(scenes, dict) or not isinstance(stats, dict):
            result["status"] = "invalid_manifest"
            return result
        exact = info.get("nodeBindings")
        if isinstance(exact, list) and exact:
            identities = set()
            for item in exact:
                if (not isinstance(item, dict) or
                    not isinstance(item.get("sceneId"), str) or
                    not isinstance(item.get("nodeId"), int) or
                    not isinstance(item.get("imageComponentId"), int) or
                    item.get("spriteFile") not in files):
                    result["status"] = "invalid_manifest"
                    return result
                key = (item["sceneId"], item["nodeId"])
                if key in identities:
                    result["status"] = "invalid_manifest"
                    return result
                identities.add(key)
            mapped = len(identities)
        else:
            mapped = sum(len(nodes) for nodes in scenes.values()
                         if isinstance(nodes, dict))
        result.update(status="ready", sprite_count=stats.get("sprite_images_exported", 0),
                      mapped_nodes=mapped, file_count=len(files))
    except (OSError, UnicodeError, ValueError, TypeError):
        result["status"] = "invalid_manifest"
    return result


SPINE_ROOT = ROOT / "output/local-spine"
SPINE_RUNTIME_ROOT = ROOT / "output/local-spine-runtime"
SPINE_FILE = re.compile(r"[0-9a-f]{24}/[A-Za-z0-9][A-Za-z0-9_.-]{0,124}\.(?:png|webp|atlas|json)", re.I)


def local_spine_resource(pathname):
    """Serve only packaged skeleton/atlas/textures listed by a validated local manifest."""
    if not pathname.startswith("/local-spine/"):
        return None
    manifest_path = SPINE_ROOT / "manifest.json"
    if not manifest_path.is_file() or manifest_path.is_symlink():
        return None
    try:
        manifest = json.loads(manifest_path.read_text(encoding="utf-8"))
        files = manifest.get("files")
        if manifest.get("version") != 1 or not isinstance(files, list) or len(files) > 2500:
            return None
        if any(not isinstance(name, str) or not SPINE_FILE.fullmatch(name) for name in files):
            return None
    except (OSError, UnicodeError, ValueError):
        return None
    if pathname == "/local-spine/manifest.json":
        return manifest_path, "application/json; charset=utf-8"
    path = pathname.removeprefix("/local-spine/")
    if path not in files or not SPINE_FILE.fullmatch(path):
        return None
    target = SPINE_ROOT / path
    if target.parent.is_symlink() or target.is_symlink() or not target.is_file():
        return None
    mime = ("application/json; charset=utf-8" if path.endswith(".json") else
            "text/plain; charset=utf-8" if path.endswith(".atlas") else
            "image/webp" if path.endswith(".webp") else "image/png")
    return target, mime


ALLOWED = {
    "/": (ROOT / "web-ui-viewer/index.html", "text/html; charset=utf-8"),
    "/index.html": (ROOT / "web-ui-viewer/index.html", "text/html; charset=utf-8"),
    "/app.js": (ROOT / "web-ui-viewer/app.js", "text/javascript; charset=utf-8"),
    "/spine-viewer": (ROOT / "web-ui-viewer/spine-viewer.html", "text/html; charset=utf-8"),
    "/spine-viewer.js": (ROOT / "web-ui-viewer/spine-viewer.js", "text/javascript; charset=utf-8"),
    "/spine-viewer.css": (ROOT / "web-ui-viewer/spine-viewer.css", "text/css; charset=utf-8"),
    # Optional licensed Spine Player 3.8 runtime, supplied by the user locally.
    "/spine-player.js": (SPINE_RUNTIME_ROOT / "spine-player.js", "text/javascript; charset=utf-8"),
    "/spine-player.css": (SPINE_RUNTIME_ROOT / "spine-player.css", "text/css; charset=utf-8"),
    "/asset-matching.js": (ROOT / "web-ui-viewer/asset-matching.js", "text/javascript; charset=utf-8"),
    "/style.css": (ROOT / "web-ui-viewer/style.css", "text/css; charset=utf-8"),
    "/ui-scenes.json": (
        ROOT / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json",
        "application/json; charset=utf-8"
    ),
}

class ViewerHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        # Never expose the repo, user's files, local XAPK, or arbitrary paths.
        pathname = self.path.split("?", 1)[0]
        if pathname == "/local-art/status.json":
            payload = json.dumps(local_art_status(), ensure_ascii=False).encode("utf-8")
            self.send_response(200)
            self.send_header("Content-Type", "application/json; charset=utf-8")
            self.send_header("Content-Length", str(len(payload)))
            self.send_header("Cache-Control", "no-store")
            self.send_header("X-Content-Type-Options", "nosniff")
            self.end_headers()
            self.wfile.write(payload)
            return
        asset = ALLOWED.get(pathname) or local_art_resource(pathname) or local_spine_resource(pathname)
        if asset is None:
            self.send_error(404, "Not available")
            return
        filepath, mimetype = asset
        try:
            data = filepath.read_bytes()
        except FileNotFoundError:
            self.send_error(503, "UI metadata missing: run the generator first")
            return
        self.send_response(200)
        self.send_header("Content-Type", mimetype)
        self.send_header("Content-Length", str(len(data)))
        self.send_header("Cache-Control", "no-store")
        self.send_header("X-Content-Type-Options", "nosniff")
        self.send_header("Cross-Origin-Resource-Policy", "same-origin")
        self.send_header("Content-Security-Policy",
                         "default-src 'self'; style-src 'self'; "
                         "script-src 'self'; connect-src 'self'; "
                         "img-src 'self' blob:; object-src 'none'; frame-ancestors 'none'")
        self.end_headers()
        self.wfile.write(data)

    def log_message(self, format, *args):
        print("[Viewer] " + format % args)

def make_server(port=8765):
    if not 1024 <= port <= 65535:
        raise ValueError("Port must be 1024-65535")
    return ThreadingHTTPServer(("127.0.0.1", port), ViewerHandler)

def main():
    p = argparse.ArgumentParser()
    p.add_argument("--port", type=int, default=8765)
    p.add_argument("--no-browser", action="store_true")
    args = p.parse_args()
    if not ALLOWED["/ui-scenes.json"][0].is_file():
        p.error("Missing ui-scenes.json. Run: py tools/build_unity_viewer_data.py --repo-root .")
    try:
        server = make_server(args.port)
    except OSError as e:
        p.error(f"Cannot open port {args.port}: {e}. Try --port 8766.")
    url = f"http://127.0.0.1:{server.server_port}/"
    print("OFFLINE WEB UI VIEWER (khong can Unity)", flush=True)
    print("Mo: " + url, flush=True)
    print("Nhan Ctrl+C de dung. Khong ket noi game server.", flush=True)
    if not args.no_browser:
        threading.Timer(0.6, lambda: webbrowser.open(url)).start()
    try:
        server.serve_forever(poll_interval=0.3)
    except KeyboardInterrupt:
        pass
    finally:
        server.server_close()

if __name__ == "__main__":
    main()
