# Scene visual dependency audit

Four screenshots correspond to five serialized scene candidates.
This is a static asset dependency inventory, not an actual game render.

| Reference | Images linked | Textures | Spine component | Unlinked Images |
|---|---:|---:|---:|---:|
| REF01-ship-upgrade | 32 | 17 | 3 | 7 |
| REF02-hero-detail | 574 | 113 | 19 | 46 |
| REF03-islands-map-A | 43 | 30 | 1 | 1 |
| REF03-islands-map-B | 49 | 32 | 1 | 1 |
| REF04-home-crew | 265 | 88 | 22 | 34 |

## Required checks before claiming visual reconstruction

- Spine skins and playback tracks: UNKNOWN (MonoBehaviour serialized fields unavailable).
- Image null pointers: not automatically errors or runtime assignments.
- Device pixel resolution and CanvasScaler sizing: NOT CONFIRMED.
- REF03 scene variants A and B: NOT DISTINGUISHED at runtime.
- Source images and animation are NOT part of this report.

## Metadata exports

- scene-image-texture-links.csv: per-image Sprite and Texture2D identity
- scene-spine-components.csv: Spine classes and hierarchy paths
- scene-unresolved-images.csv: unlinked UI Images by scene
- scene-visual-summary.json: counts and explicit unknowns
