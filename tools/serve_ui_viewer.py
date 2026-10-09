#!/usr/bin/env python3
"""Local-only offline UI viewer with a strict allowlist of public metadata files.

No network download, authentication, game server, APK or Unity installation.
Serves only HTML/CSS/JS and pre-generated text JSON over 127.0.0.1.
"""
from __future__ import annotations

import argparse
from functools import partial
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
import threading
import webbrowser

ROOT = Path(__file__).resolve().parents[1]
ALLOWED = {
    "/": (ROOT / "web-ui-viewer/index.html", "text/html; charset=utf-8"),
    "/index.html": (ROOT / "web-ui-viewer/index.html", "text/html; charset=utf-8"),
    "/app.js": (ROOT / "web-ui-viewer/app.js", "text/javascript; charset=utf-8"),
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
        asset = ALLOWED.get(pathname)
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
        self.send_header("Content-Security-Policy",
                         "default-src 'self'; style-src 'self'; "
                         "script-src 'self'; connect-src 'self'; "
                         "img-src 'none'; object-src 'none'; frame-ancestors 'none'")
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
