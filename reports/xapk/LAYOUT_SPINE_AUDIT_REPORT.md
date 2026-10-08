# CanvasScaler / Spine / Image pointer audit

Static metadata only. Runtime sizes, layout and animation remain unverified.

## Canvas

| Metric | Count |
|---|---:|
| CanvasScaler components | 76 |
| Attached to Canvas GameObject | 76 |
| Reference resolution read from explicit typetree | 0 |

## Unlinked Images

| Classification | Count |
|---|---:|
| serialized_null_sprite_pointer | 2756 |
| uncalibrated_offset | 1 |

A serialized null pointer can reflect an intentionally empty Image or runtime setting.
It does NOT automatically indicate an error or dynamic sprite assignment.

## Spine components

| Class | Instances |
|---|---:|
| Spine.Unity.SkeletonGraphic | 664 |
| Spine.Unity.SpineAtlasAsset | 522 |
| Spine.Unity.SkeletonDataAsset | 522 |
| SpineObject | 383 |
| Spine.Unity.SkeletonAnimation | 30 |
| Spine.Unity.SkeletonUtilityBone | 4 |
| Spine.Unity.AnimationReferenceAsset | 1 |

## Additional outputs

- image-null-pointer-audit.csv
- canvas-spine-components.csv
- animation-clip-properties.csv
- layout-spine-summary.json
