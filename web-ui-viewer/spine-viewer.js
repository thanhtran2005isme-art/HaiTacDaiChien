"use strict";

/* Local-only Spine 3.8 preview. Requires authorized official Spine Player 3.8.
   No external CDN, no fabricated tweening, no inferred scene-character mapping. */
const $ = id => document.getElementById(id);
const PACK = /^[0-9a-f]{24}$/;
const FILE = /^[0-9a-f]{24}\/[A-Za-z0-9][A-Za-z0-9_.-]{0,124}\.(?:atlas|json|png|webp)$/i;
let packs = [];
let player = null;
let runtimeReady = false;

function status(message) {
  $("status").textContent = message;
}
function localFile(filename) {
  if (!FILE.test(filename)) throw new Error("Tên tài nguyên Spine không hợp lệ.");
  return "/local-spine/" + filename;
}
function selectedPack() {
  return packs.find(p => p.id === $("pack").value);
}
function fillAnimations() {
  const pack = selectedPack();
  $("animation").replaceChildren();
  for (const name of pack?.animations || []) {
    const option = document.createElement("option");
    option.value = name;
    option.textContent = name;
    $("animation").appendChild(option);
  }
}
function play() {
  if (!runtimeReady) {
    status("Chưa có Spine Player 3.8 tương thích. Xem README để cung cấp runtime hợp pháp trên máy.");
    return;
  }
  const pack = selectedPack();
  if (!pack) return;
  if (player && typeof player.dispose === "function") player.dispose();
  $("player-stage").replaceChildren();
  try {
    const animation = $("animation").value;
    player = new window.spine.SpinePlayer("player-stage", {
      skeleton: localFile(pack.skeleton),
      atlas: localFile(pack.atlas),
      animation,
      alpha: true,
      backgroundColor: "#00000000",
      showControls: true,
      success: () => status("Đang phát " + pack.name + " · " + animation + " (Spine " + pack.version + ")"),
      error: (_, reason) => status("Spine Player không thể dựng bộ này: " + String(reason)),
    });
  } catch (error) {
    status("Không thể khởi động Spine runtime: " + error.message);
  }
}
function loadRuntime() {
  if (window.spine?.SpinePlayer) {
    runtimeReady = true;
    status("Đã tải dữ liệu và Spine Player 3.8. Chọn animation để xem.");
    play();
    return;
  }
  const script = document.createElement("script");
  script.src = "/spine-player.js";
  script.onload = () => {
    runtimeReady = typeof window.spine?.SpinePlayer === "function";
    if (!runtimeReady) {
      status("Spine Player 3.8 không cung cấp spine.SpinePlayer. Kiểm tra bản runtime.");
      return;
    }
    play();
  };
  script.onerror = () => status("Thiếu Spine Player 3.8 tại output/local-spine-runtime/spine-player.js. Chưa thể phát animation.");
  document.head.appendChild(script);
}
async function bootstrap() {
  $("play").addEventListener("click", play);
  $("pack").addEventListener("change", () => { fillAnimations(); if (runtimeReady) play(); });
  $("animation").addEventListener("change", () => { if (runtimeReady) play(); });
  const response = await fetch("/local-spine/manifest.json", {cache: "no-store"});
  if (!response.ok) {
    status("Chưa có skeleton/atlas/texture đã xuất. Chạy: py -3 tools/export_local_spine.py --name Ace");
    return;
  }
  const manifest = await response.json();
  if (manifest.version !== 1 || !Array.isArray(manifest.packages)) {
    throw new Error("Manifest Spine không đúng phiên bản.");
  }
  packs = manifest.packages.filter(p =>
    PACK.test(p.id) && typeof p.name === "string" &&
    typeof p.version === "string" && p.version.startsWith("3.8.") &&
    Array.isArray(p.animations) && p.animations.length &&
    FILE.test(p.skeleton) && FILE.test(p.atlas) &&
    p.skeleton.startsWith(p.id + "/") && p.atlas.startsWith(p.id + "/")
  );
  if (!packs.length) {
    status("Chưa có bộ Spine 3.8 hoàn chỉnh (skeleton + atlas + texture).");
    return;
  }
  for (const pack of packs) {
    const opt = document.createElement("option");
    opt.value = pack.id;
    opt.textContent = pack.name + " (" + pack.version + ")";
    $("pack").appendChild(opt);
  }
  fillAnimations();
  status("Đã nhận " + packs.length + " bộ Spine có ảnh atlas. Đang kiểm tra runtime...");
  loadRuntime();
}
bootstrap().catch(error => status("Lỗi đọc bộ Spine local: " + String(error.message)));
