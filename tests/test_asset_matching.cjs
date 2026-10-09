"use strict";
const assert = require("node:assert/strict");
const matcher = require("../web-ui-viewer/asset-matching.js");

function file(name, size = 1024, type = "image/png") {
  return { name, size, type };
}
assert.equal(matcher.key("Logo Ship.PNG"), "logo_ship");
assert.equal(matcher.key("a\\b\\NÁMI-ROBIN.webp"), "námi_robin");
assert.equal(matcher.key("  Icon__01  .png"), "icon_01");
assert.equal(matcher.key("../IMG_CAPTAIN.JpEg"), "img_captain");

assert.equal(matcher.supported(file("bgr.png")), true);
assert.equal(matcher.supported(file("icon.webp", 200, "image/webp")), true);
assert.equal(matcher.supported(file("sprite.svg", 300, "image/svg+xml")), false);
assert.equal(matcher.supported(file("a.png", 100, "text/html")), false);
assert.equal(matcher.supported(file("../foo.png")), true); // basename is sanitized for matching

const goodA = file("bgr_monster.PNG", 800, "image/png");
const goodB = file("Btn-Upgrade.webp", 1200, "image/webp");
let index = matcher.buildIndex([goodA, goodB, file("alert.svg", 1000)]);
assert.equal(index.accepted, 2);
assert.equal(index.rejected, 1);
assert.equal(index.byKey.size, 2);
assert.equal(matcher.find({ sprites: ["bgr_monster"] }, index), goodA);
assert.equal(matcher.find({ sprites: ["btn_upgrade"] }, index), goodB);
assert.equal(matcher.find({ sprites: ["bgr"] }, index), null); // no fuzzy match

index = matcher.buildIndex([
  file("icon_a.png", 100), file("Icon-A.webp", 100),
  file("independent.jpg", 100, "image/jpeg")
]);
assert.equal(index.collisions.size, 1);
assert.equal(index.byKey.size, 1);
assert.equal(matcher.find({sprites: ["icon_a"]}, index), null);
assert.equal(matcher.find({sprites: ["independent"]}, index)?.name, "independent.jpg");

index = matcher.buildIndex([
  file("zero.png", 0),
  file("huge.png", matcher.MAX_EACH_BYTES + 1),
  file("valid.jpg", 128, "image/jpeg")
]);
assert.equal(index.rejected, 2);
assert.equal(index.accepted, 1);

const lots = Array.from({length: matcher.MAX_FILES + 10}, (_, i) =>
  file("sprite" + i + ".png", 1024));
index = matcher.buildIndex(lots);
assert.equal(index.accepted, matcher.MAX_FILES);
assert.equal(index.rejected, 10);

assert.equal(matcher.find({}, matcher.buildIndex([])), null);
console.log("PASS: 17 local matching, input safety, collision and limit assertions");
