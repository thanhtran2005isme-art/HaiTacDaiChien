"use strict";

/* Offline, approximate RectTransform layout viewer.
   No game asset binaries, external requests, source-code eval or runtime game APIs. */
const VIRTUAL_W = 1600, VIRTUAL_H = 900;
const MAX_DRAWN = 780;
const $ = id => document.getElementById(id);
const state = {
  database: null, scene: 0, filter: "all", showInactive: true,
  zoom: 1, selectedId: null, demoLevel: 1,
  geometry: new Map(), elements: new Map(),
  assets: null, autoArt: null, manualAssets: new Map(), referenceImages: new Map(),
  objectUrls: new Map(),
  boundsVisible: false,
};

const sceneList = $("scene-list");
const stage = $("stage");
const layer = $("node-layer");
const frame = $("canvas-frame");
const inspector = $("inspector");


/* Local File objects are kept only in browser memory. Files never hit HTTP. */
function nodeAssetId(node) {
  if (!node || !state.database) return null;
  return sceneData().id + ":" + node.id;
}

function imageFileFor(node) {
  return state.manualAssets.get(nodeAssetId(node)) ||
    window.AssetMatcher.find(node, state.assets);
}

function autoArtFor(node) {
  if (!node || !state.database) return null;
  const name = state.autoArt?.scenes?.[sceneData().id]?.[node.path];
  return typeof name === "string" && /^[0-9a-f]{32}\.png$/.test(name) &&
    state.autoArt.files?.includes(name) ? name : null;
}

function imageUrlFor(file) {
  if (!state.objectUrls.has(file))
    state.objectUrls.set(file, URL.createObjectURL(file));
  return state.objectUrls.get(file);
}

function revokeImages() {
  for (const url of state.objectUrls.values()) URL.revokeObjectURL(url);
  state.objectUrls.clear();
}

function updateVisualStatus(drawn = null) {
  if (!state.database) return;
  const scene = sceneData();
  let matched = 0, automatic = 0;
  for (const node of scene.nodes) {
    if (imageFileFor(node)) matched++;
    else if (autoArtFor(node)) automatic++;
  }
  const summary = state.assets;
  const parts = [automatic + " nút ghép tự động", matched + " nút dùng ảnh đã chọn"];
  if (state.referenceImages.has(scene.id)) parts.push("Đã gắn ảnh chụp nền");
  if (drawn !== null) parts.push(drawn + " vùng hiển thị");
  if (summary) {
    parts.push(summary.accepted + " ảnh đã chọn");
    if (summary.collisions.size) parts.push(summary.collisions.size + " tên trùng đã bỏ qua");
    if (summary.rejected) parts.push(summary.rejected + " file không hợp lệ/vượt giới hạn");
  }
  $("visual-info").textContent = (matched + automatic) ?
    "Đang hiển thị ảnh từ dữ liệu local. " + parts.join(" · ") +
      ". Bố cục vẫn là ước tính từ RectTransform." :
    "Chưa có ảnh tự động cho scene này. " + parts.join(" · ") +
      ". Chạy CHUAN_BI_DO_HOA.bat để giải mã XAPK, hoặc gán ảnh thủ công.";
}

function refreshArt() {
  if (!state.database) return;
  refreshReference();
  renderNodes();
  const node = sceneData().nodes.find(n => n.id === state.selectedId);
  selectNode(node || null);
}

function handleSelectedFolder(event) {
  const files = event.target.files;
  if (!files?.length) return;
  // Replacing the folder explicitly resets temporary assignments, by design.
  revokeImages();
  state.manualAssets.clear();
  state.assets = window.AssetMatcher.buildIndex(files);
  $("asset-status").textContent =
    "Đã nhận " + state.assets.accepted + " ảnh hợp lệ; " +
    state.assets.byKey.size + " tên Sprite không trùng. " +
    (state.assets.collisions.size ? "Bỏ qua " + state.assets.collisions.size + " tên trùng. " : "") +
    (state.assets.rejected ? state.assets.rejected + " file bị bỏ qua. " : "") +
    "Không gửi ảnh lên máy chủ.";
  event.target.value = "";
  refreshArt();
}

function clearAssets() {
  revokeImages();
  state.assets = null;
  state.manualAssets.clear();
  $("asset-status").textContent = "Chưa nạp ảnh. Hiển thị wireframe.";
  refreshArt();
}

function handleManualImage(event) {
  const file = event.target.files?.[0];
  const node = sceneData()?.nodes.find(n => n.id === state.selectedId);
  event.target.value = "";
  if (!node || !file) return;
  if (!window.AssetMatcher.supported(file) || file.size <= 0 ||
      file.size > window.AssetMatcher.MAX_EACH_BYTES) {
    $("node-asset-status").textContent =
      "Chỉ nhận PNG/JPG/WebP nhỏ hơn 20 MB cho mỗi ảnh.";
    return;
  }
  revokeImages();
  state.manualAssets.set(nodeAssetId(node), file);
  refreshArt();
}

function clearManualImage() {
  const node = sceneData()?.nodes.find(n => n.id === state.selectedId);
  if (!node) return;
  revokeImages();
  state.manualAssets.delete(nodeAssetId(node));
  refreshArt();
}


function refreshReference() {
  if (!state.database) return;
  const file = state.referenceImages.get(sceneData().id);
  const image = $("reference-image");
  if (file) {
    image.src = imageUrlFor(file);
    image.hidden = false;
    stage.classList.add("has-reference");
  } else {
    image.hidden = true;
    image.removeAttribute("src");
    stage.classList.remove("has-reference");
  }
}

function handleReferenceImage(event) {
  const file = event.target.files?.[0];
  event.target.value = "";
  if (!file || !state.database) return;
  if (!window.AssetMatcher.supported(file) || file.size <= 0 ||
      file.size > window.AssetMatcher.MAX_EACH_BYTES) {
    $("visual-info").textContent =
      "Ảnh chụp cần có định dạng PNG/JPG/WebP và nhỏ hơn 20 MB.";
    return;
  }
  revokeImages();
  state.referenceImages.set(sceneData().id, file);
  refreshArt();
}

function clearReferenceImage() {
  if (!state.database) return;
  revokeImages();
  state.referenceImages.delete(sceneData().id);
  refreshArt();
}

function v2(value, fallback) {
  return Array.isArray(value) && value.length === 2 &&
    value.every(Number.isFinite) ? value : fallback;
}

function computeRect(parent, node) {
  const amin = v2(node.a0, [.5, .5]);
  const amax = v2(node.a1, [.5, .5]);
  const pivot = v2(node.pivot, [.5, .5]);
  const delta = v2(node.delta, [0, 0]);
  const pos = v2(node.position, [0, 0]);
  const w = parent.w * (amax[0] - amin[0]) + delta[0];
  const h = parent.h * (amax[1] - amin[1]) + delta[1];
  const x = parent.x + parent.w *
    (amin[0] + (amax[0] - amin[0]) * pivot[0]) + pos[0] - w * pivot[0];
  const y = parent.y + parent.h *
    (amin[1] + (amax[1] - amin[1]) * pivot[1]) + pos[1] - h * pivot[1];
  if (![w, h, x, y].every(Number.isFinite))
    return null;
  return { x, y, w, h };
}

function classify(node) {
  if (node.sprites?.length) return "image";
  if (node.missingImages > 0) return "missing";
  if (node.spine?.length) return "spine";
  if (node.types?.includes("Button")) return "button";
  if (node.types?.some(x => x === "Text" || x.includes("TextMeshPro"))) return "spine";
  return null;
}

function filterMatches(node) {
  if (!state.showInactive && !node.active) return false;
  switch (state.filter) {
    case "image": return !!node.sprites?.length;
    case "button": return !!node.types?.includes("Button");
    case "spine": return !!node.spine?.length;
    case "missing": return node.missingImages > 0;
    default: return true;
  }
}

function sceneData() { return state.database.scenes[state.scene]; }

function showError(error) {
  const box = $("error-box");
  box.hidden = false;
  box.textContent = "Không thể chạy UI Viewer: " + (error?.message || String(error)) +
    ". Hãy cập nhật GitHub và kiểm tra Python, rồi chạy lại CHAY_UI_OFFLINE.bat.";
}

function fitStage() {
  const scale = Math.min((frame.clientWidth - 24) / VIRTUAL_W,
                         (frame.clientHeight - 30) / VIRTUAL_H);
  const actual = Math.max(.05, scale) * state.zoom;
  stage.style.transform = "translate(-50%, -50%) scale(" + actual.toFixed(5) + ")";
  $("zoom-label").textContent = Math.round(state.zoom * 100) + "%";
}

function updateStats(scene) {
  $("scene-title").textContent = scene.title;
  $("scene-description").textContent = scene.confidence +
    " · " + scene.source + " · khung 1600×900 giả định";
  $("stat-nodes").textContent = scene.nodeCount.toLocaleString("vi-VN");
  $("stat-sprite").textContent = scene.linkedImageEntries.toLocaleString("vi-VN");
  $("stat-missing").textContent = scene.unlinkedImageEntries.toLocaleString("vi-VN");
  $("scene-count").textContent = state.database.scenes.length + " scene ứng viên";
}

function switchScene(index) {
  const count = state.database.scenes.length;
  state.scene = (index + count) % count;
  state.selectedId = null;
  state.zoom = 1;
  state.geometry.clear();
  state.elements.clear();
  const scene = sceneData();
  updateStats(scene);
  refreshReference();
  for (const [i, child] of [...sceneList.children].entries()) {
    child.classList.toggle("selected", i === state.scene);
    child.setAttribute("aria-current", i === state.scene ? "page" : "false");
  }
  renderNodes();
  showInspector(null);
  updateMissing();
  updateSearch();
  fitStage();
}

function makeSceneMenu() {
  sceneList.replaceChildren();
  state.database.scenes.forEach((scene, i) => {
    const button = document.createElement("button");
    button.type = "button";
    button.className = "scene-button";
    const n = document.createElement("span");
    n.className = "scene-number";
    n.textContent = (i + 1).toString().padStart(2, "0");
    const text = document.createElement("span");
    text.className = "scene-text";
    text.textContent = scene.title;
    const sub = document.createElement("span");
    sub.className = "scene-sub";
    sub.textContent = scene.nodeCount + " nút UI";
    text.appendChild(sub);
    button.append(n, text);
    button.addEventListener("click", () => switchScene(i));
    sceneList.appendChild(button);
  });
}

function renderNodes() {
  const scene = sceneData();
  const fragment = document.createDocumentFragment();
  state.geometry.clear();
  state.elements.clear();
  let drawn = 0;
  for (const node of scene.nodes) {
    if (node.id === scene.rootTransform) {
      state.geometry.set(node.id, { x: 0, y: 0, w: VIRTUAL_W, h: VIRTUAL_H });
      continue;
    }
    const parent = state.geometry.get(node.parent);
    if (!parent) continue;
    const rect = computeRect(parent, node);
    if (!rect || rect.w <= 0 || rect.h <= 0) {
      // A 0-size container can receive runtime LayoutGroup dimensions.
      state.geometry.set(node.id, parent);
      continue;
    }
    state.geometry.set(node.id, rect);
    const file = imageFileFor(node);
    const generated = autoArtFor(node);
    const type = classify(node) || (file || generated ? "image" : null);
    if (!type || !filterMatches(node)) continue;
    if (rect.w < 2 || rect.h < 2 ||
        rect.x > VIRTUAL_W || rect.y > VIRTUAL_H ||
        rect.x + rect.w < 0 || rect.y + rect.h < 0) continue;
    if (++drawn > MAX_DRAWN) break;

    const box = document.createElement("button");
    box.type = "button";
    box.className = "wire-node " + type + (node.active ? "" : " inactive");
    box.style.left = rect.x.toFixed(2) + "px";
    box.style.top = (VIRTUAL_H - rect.y - rect.h).toFixed(2) + "px";
    box.style.width = rect.w.toFixed(2) + "px";
    box.style.height = rect.h.toFixed(2) + "px";
    box.style.zIndex = Math.min(40, Math.round(drawn / 30)).toString();
    box.title = node.path + (node.sprites?.length ?
      " | " + node.sprites.join(", ") : "");
    box.setAttribute("aria-label", node.name + " — " + type);
    if (file || generated) {
      const img = document.createElement("img");
      img.src = file ? imageUrlFor(file) : "/local-art/" + generated;
      img.alt = "";
      img.decoding = "async";
      box.classList.add("asset-loaded");
      if (/bgr|background|backdrop|wallpaper|scenery/i.test(node.name))
        box.classList.add("cover");
      box.appendChild(img);
    }
    if (rect.w > 115 && rect.h > 26 && drawn < 175) {
      const caption = document.createElement("span");
      caption.textContent = node.name.slice(0, 37);
      box.appendChild(caption);
    }
    box.addEventListener("click", event => {
      event.stopPropagation();
      selectNode(node);
    });
    state.elements.set(node.id, box);
    fragment.appendChild(box);
  }
  layer.replaceChildren(fragment);
  updateVisualStatus(drawn);
}

function selectNode(node) {
  state.selectedId = node?.id ?? null;
  for (const [id, element] of state.elements.entries()) {
    element.classList.toggle("selected", id === state.selectedId);
  }
  showInspector(node);
}

function inspectorRow(label, value) {
  const outer = document.createElement("div");
  const title = document.createElement("b");
  title.textContent = label + ": ";
  const content = document.createElement("span");
  content.textContent = value == null ? "Chưa xác minh" :
    Array.isArray(value) ? value.join(", ") || "—" : String(value);
  outer.append(title, content);
  return outer;
}

function showInspector(node) {
  inspector.replaceChildren();
  const scene = sceneData();
  $("node-image").disabled = !node;
  $("node-image-clear").disabled = !node || !state.manualAssets.has(nodeAssetId(node));
  $("node-asset-status").textContent = node ?
    (state.manualAssets.has(nodeAssetId(node)) ?
      "Đang dùng ảnh gán thủ công: " + state.manualAssets.get(nodeAssetId(node)).name :
      "Có thể chọn ảnh PNG/JPG/WebP cho nút này.") : "Chưa chọn nút.";
  inspector.append(
    inspectorRow("Màn hình", scene.title),
    inspectorRow("Trạng thái", scene.confidence),
    inspectorRow("Nút cấu trúc", scene.nodeCount),
    inspectorRow("Sprite liên kết", scene.linkedImageEntries),
    inspectorRow("Image chưa liên kết", scene.unlinkedImageEntries)
  );
  if (!node) {
    const info = document.createElement("p");
    info.className = "hint";
    info.textContent = "Chọn ô màu trên bản xem trước hoặc tìm GameObject bên trái để xem chi tiết.";
    inspector.appendChild(info);
    inspector.append(
      inspectorRow("Demo tàu (không phải dữ liệu game)", state.demoLevel),
      inspectorRow("Skin Spine runtime", "Không xác định"),
      inspectorRow("CanvasScaler runtime", "Chưa xác minh")
    );
    return;
  }
  const div = document.createElement("hr");
  inspector.appendChild(div);
  inspector.append(
    inspectorRow("GameObject", node.name),
    inspectorRow("Đường dẫn UI", node.path),
    inspectorRow("Transform ID", node.id),
    inspectorRow("Parent ID", node.parent),
    inspectorRow("Active trong asset", node.active ? "Có" : "Không"),
    inspectorRow("Component", node.types),
    inspectorRow("Sprite ứng viên", node.sprites),
    inspectorRow("Ảnh đang ghép", imageFileFor(node)?.name || (autoArtFor(node) ? "Sprite tự giải mã trên máy" : "Chưa có ảnh")),
    inspectorRow("Spine class", node.spine),
    inspectorRow("Image thiếu", node.missingImages),
    inspectorRow("Anchor min/max", JSON.stringify([node.a0, node.a1])),
    inspectorRow("Pivot", node.pivot),
    inspectorRow("SizeDelta", node.delta),
    inspectorRow("AnchoredPosition", node.position),
    inspectorRow("Scale/rotation", JSON.stringify([node.scale, node.rotationZ]))
  );
}

function updateMissing() {
  const root = $("missing-list");
  root.replaceChildren();
  const missing = sceneData().nodes.filter(n => n.missingImages > 0);
  for (const node of missing.slice(0, 45)) {
    const button = document.createElement("button");
    button.className = "missing-item";
    button.textContent = "⚠ " + node.name + " · " + node.missingImages;
    button.title = node.path;
    button.addEventListener("click", () => selectNode(node));
    root.appendChild(button);
  }
  if (missing.length > 45) {
    const caption = document.createElement("small");
    caption.textContent = "Và " + (missing.length - 45) +
      " node khác. Dùng ô tìm kiếm để tra cứu.";
    root.appendChild(caption);
  }
  if (!missing.length) {
    const note = document.createElement("span");
    note.textContent = "Không có Image thiếu trong dataset.";
    root.appendChild(note);
  }
}

function updateSearch() {
  const results = $("search-results");
  results.replaceChildren();
  const term = $("search").value.trim().toLocaleLowerCase("vi");
  if (!term) return;
  const hits = sceneData().nodes.filter(node =>
    [node.name, node.path, ...(node.sprites || []), ...(node.spine || [])]
      .some(x => x.toLocaleLowerCase("vi").includes(term))).slice(0, 30);
  for (const node of hits) {
    const button = document.createElement("button");
    button.type = "button";
    button.className = "search-result";
    const name = document.createElement("span");
    name.textContent = node.name;
    const path = document.createElement("small");
    path.textContent = node.path;
    button.append(name, path);
    button.addEventListener("click", () => selectNode(node));
    results.appendChild(button);
  }
  if (!hits.length) {
    const note = document.createElement("p");
    note.className = "empty-results";
    note.textContent = "Không tìm thấy nút phù hợp.";
    results.appendChild(note);
  }
}

function registerEvents() {
  $("search").addEventListener("input", updateSearch);
  $("asset-folder").addEventListener("change", handleSelectedFolder);
  $("asset-files").addEventListener("change", handleSelectedFolder);
  $("asset-clear").addEventListener("click", clearAssets);
  $("reference-file").addEventListener("change", handleReferenceImage);
  $("reference-clear").addEventListener("click", clearReferenceImage);
  $("node-image").addEventListener("change", handleManualImage);
  $("node-image-clear").addEventListener("click", clearManualImage);
  $("show-bounds").addEventListener("change", event => {
    state.boundsVisible = event.target.checked;
    stage.classList.toggle("no-bounds", !state.boundsVisible);
  });
  $("show-inactive").addEventListener("change", event => {
    state.showInactive = event.target.checked;
    renderNodes();
    if (state.selectedId !== null)
      selectNode(sceneData().nodes.find(n => n.id === state.selectedId));
  });
  document.querySelectorAll(".filter").forEach(button => {
    button.addEventListener("click", () => {
      state.filter = button.dataset.filter;
      document.querySelectorAll(".filter").forEach(b =>
        b.classList.toggle("active", b === button));
      renderNodes();
      if (state.selectedId !== null)
        selectNode(sceneData().nodes.find(n => n.id === state.selectedId));
    });
  });
  $("zoom-in").addEventListener("click", () => {
    state.zoom = Math.min(3, Math.round((state.zoom + .2) * 10) / 10);
    fitStage();
  });
  $("zoom-out").addEventListener("click", () => {
    state.zoom = Math.max(.4, Math.round((state.zoom - .2) * 10) / 10);
    fitStage();
  });
  $("zoom-reset").addEventListener("click", () => { state.zoom = 1; fitStage(); });
  $("demo-upgrade").addEventListener("click", () => {
    state.demoLevel++;
    $("demo-level").textContent = "Tàu cấp thử nghiệm: " + state.demoLevel;
    showInspector(sceneData().nodes.find(n => n.id === state.selectedId));
  });
  $("demo-reset").addEventListener("click", () => {
    state.demoLevel = 1;
    $("demo-level").textContent = "Tàu cấp thử nghiệm: 1";
    showInspector(sceneData().nodes.find(n => n.id === state.selectedId));
  });
  window.addEventListener("keydown", event => {
    if (event.target instanceof HTMLInputElement) return;
    if (event.key === "ArrowRight") switchScene(state.scene + 1);
    if (event.key === "ArrowLeft") switchScene(state.scene - 1);
  });
  if (typeof ResizeObserver !== "undefined")
    new ResizeObserver(fitStage).observe(frame);
  else window.addEventListener("resize", fitStage);
}

async function bootstrap() {
  registerEvents();
  stage.classList.add("no-bounds");
  const response = await fetch("/ui-scenes.json", { cache: "no-store" });
  if (!response.ok) throw new Error("Không lấy được ui-scenes.json (" + response.status + ")");
  const db = await response.json();
  if (db.schemaVersion !== 1 || !Array.isArray(db.scenes) || db.scenes.length !== 5)
    throw new Error("Dữ liệu UI không đúng phiên bản.");
  for (const scene of db.scenes) {
    if (!Array.isArray(scene.nodes) || scene.nodes.length !== scene.nodeCount ||
        !scene.nodes.find(n => n.id === scene.rootTransform))
      throw new Error("Cây UI không hợp lệ: " + scene.id);
  }
  state.database = db;
  try {
    const artResponse = await fetch("/local-art/manifest.json", {cache: "no-store"});
    if (artResponse.ok) {
      const manifest = await artResponse.json();
      if (manifest.version === 1 && Array.isArray(manifest.files) &&
          manifest.scenes && typeof manifest.scenes === "object") {
        state.autoArt = manifest;
        stage.classList.add("has-auto-art");
        $("asset-status").textContent =
          "Đã có " + manifest.stats?.sprite_images_exported +
          " Sprite từ XAPK. Ảnh tự động được ưu tiên khi không gán thủ công.";
      }
    }
  } catch (_) {
    // No local extraction yet: retain the existing wireframe and manual picker.
  }
  makeSceneMenu();
  switchScene(0);
}

bootstrap().catch(showError);
