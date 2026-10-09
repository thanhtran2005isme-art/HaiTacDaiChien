/* Pure, offline-safe image matching for locally selected user-authorized files.
   Supports PNG/JPEG/WebP only; never performs network requests.
   Matching uses exact normalized Sprite basename. No fuzzy guesswork. */
(function (root, factory) {
  const api = factory();
  if (typeof module !== "undefined" && module.exports) module.exports = api;
  if (root) root.AssetMatcher = api;
})(typeof globalThis === "undefined" ? null : globalThis, function () {
  "use strict";
  const MAX_FILES = 750;
  const MAX_EACH_BYTES = 20 * 1024 * 1024;
  const MAX_TOTAL_BYTES = 250 * 1024 * 1024;
  const EXT = /\.(png|jpe?g|webp)$/i;

  function key(value) {
    if (typeof value !== "string") return "";
    const basename = value.split(/[\\/]/).pop().replace(EXT, "");
    return basename.normalize("NFKC").trim().toLocaleLowerCase("en")
      .replace(/[\s_-]+/g, "_");
  }

  function supported(file) {
    if (!file || typeof file.name !== "string" || !EXT.test(file.name)) return false;
    // Some browsers don't set the MIME type for directory entries.
    return !file.type || ["image/png", "image/jpeg", "image/webp"].includes(file.type);
  }

  function buildIndex(input) {
    const files = Array.from(input || []);
    const byKey = new Map();
    const collisions = new Set();
    let accepted = 0;
    let rejected = 0;
    let totalBytes = 0;
    for (const file of files) {
      const size = Number(file.size);
      if (!supported(file) || !Number.isFinite(size) || size <= 0 ||
          size > MAX_EACH_BYTES || accepted >= MAX_FILES ||
          totalBytes + size > MAX_TOTAL_BYTES) {
        rejected++;
        continue;
      }
      const name = key(file.name);
      if (!name) { rejected++; continue; }
      accepted++;
      totalBytes += size;
      if (collisions.has(name)) continue;
      if (byKey.has(name)) {
        byKey.delete(name);
        collisions.add(name);
      } else {
        byKey.set(name, file);
      }
    }
    return { byKey, collisions, accepted, rejected, totalBytes };
  }

  function find(node, index) {
    if (!node || !Array.isArray(node.sprites) || !index?.byKey) return null;
    for (const name of node.sprites) {
      const file = index.byKey.get(key(name));
      if (file) return file;
    }
    return null;
  }

  return { key, supported, buildIndex, find,
           MAX_FILES, MAX_EACH_BYTES, MAX_TOTAL_BYTES };
});
