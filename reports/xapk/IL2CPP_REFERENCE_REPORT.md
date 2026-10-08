# IL2CPP & Unity cross-file UI references

This is a **static metadata analysis**, not an editable game client.

## Native/metadata presence

| File | APK | Bytes | IL2CPP header |
|---|---|---:|---|
| assets/bin/Data/Managed/Metadata/global-metadata.dat | 000_com.mobi389.murom.apk | 8,855,528 | standard_header (v31) |
| assets/bin/Data/ScriptingAssemblies.json | 000_com.mobi389.murom.apk | 3,765 | — |
| lib/arm64-v8a/libil2cpp.so | 001_config.arm64_v8a.apk | 48,545,552 | — |
| lib/arm64-v8a/libunity.so | 001_config.arm64_v8a.apk | 19,125,752 | — |

## UI classes resolved through MonoScript

| Type | Components |
|---|---:|
| Image | 19,643 |
| Text | 4,479 |
| Button | 2,876 |
| Mask | 1,203 |
| Slider | 324 |
| ScrollRect | 232 |
| InputField | 109 |
| Toggle | 17 |
| Dropdown | 5 |
| Scrollbar | 4 |
| RawImage | 3 |

## Resolution outcomes

| Status | Count |
|---|---:|
| mono_script_external_resolved | 43,872 |
| ui_gameobject_local | 28,895 |
| image_typetree_unavailable | 19,646 |
| sprite_renderer_external_resolved | 61 |
| sprite_renderer_local | 57 |
| mono_script_local | 4 |
| mono_script_null | 2 |
| mono_script_external_file_missing | 1 |

## Caveats

- MonoBehaviour classification comes from MonoScript references and class names.
- Unity SpriteRenderer is not necessarily UI.Image.
- Missing type trees mean Image.sprite references can remain unresolved.
- Missing or protected IL2CPP metadata is reported, not bypassed.
- No packaged game assets or source have been committed.

See il2cpp-ref-summary.json and resolved-ui-components.csv for details.
