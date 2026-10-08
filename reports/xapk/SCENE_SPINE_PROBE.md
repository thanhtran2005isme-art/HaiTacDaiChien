# Spine dependency pointer candidates

Static Unity PPtr matches to known Spine MonoBehaviour targets, NOT a reconstructed skin/animation.

| Metric | Count |
|---|---:|
| direct_candidate_links | 22 |
| known_asset_target_components | 1044 |
| scene_components_without_direct_targets | 24 |
| scene_spine_components | 46 |
| skeleton_to_atlas_candidate_links | 17 |

## Unresolved properties

- SkeletonDataAsset references are candidates only; the original serialized field is unknown.
- Skin selection and animation track are UNKNOWN and require type reconstruction or runtime inspection.
- No downloaded assets, Spine atlas textures or frame animations were exported.

See scene-spine-asset-candidates.csv for exact pointer IDs and offsets.
