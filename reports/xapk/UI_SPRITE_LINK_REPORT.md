# Unity Image ↔ Sprite reference analysis

This scanner verifies every Sprite candidate against an actual Unity Sprite
object and its serialized-file destination. **It does not manufacture type trees.**

## Counts

| Metric | Value |
|---|---:|
| confidence_probable_m_sprite | 16,885 |
| confidence_unconfirmed_sprite_pointer | 1 |
| confidence_unresolved | 2,757 |
| image_components_from_prior_audit | 19,643 |
| images_with_one_candidate | 16,886 |
| images_without_matching_sprite | 2,757 |
| no_usable_image_typetree | 19,643 |
| serialized_files | 116 |
| total_resolved_sprite_candidate_pointers | 16,886 |

## Confidence levels

- **verified_typetree**: parsed explicit m_Sprite through type tree
- **probable_m_sprite**: unique validated Sprite pointer at an offset repeated in >=2 Image components from same serialized file
- **unconfirmed_sprite_pointer**: unique pointer without corroborated offset
- Ambiguous or missing pointers remain unlinked

## Output

See ui-image-sprite-links.csv and ui-image-sprite-summary.json.
This report is only structural research metadata, not a working game UI.
