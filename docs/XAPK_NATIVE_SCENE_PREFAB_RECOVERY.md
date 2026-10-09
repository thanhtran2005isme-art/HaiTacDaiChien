# Kiểm kê Scene/Prefab trực tiếp từ XAPK — không dựng giao diện bằng phỏng đoán

## Vấn đề phát hiện từ mã nguồn

Bộ dựng cũ `UnityCanvasReconstructor.cs` tạo Canvas/Camera preview mới, đặt
`CanvasScaler.referenceResolution = (1600,900)`, root scale=1, và đặt
`Image.Type.Simple` khi không đọc được typetree. Những giá trị này **không
phải giá trị xác minh được của XAPK**. Vì vậy có thể có Sprite đúng nhưng
màn hình REF04 vẫn bị sai vị trí, kích thước, che lớp và thiếu nhân vật.

Bộ dữ liệu `ui-scenes.json` là **năm cây UI ứng viên được chọn để nghiên cứu**,
không phải năm file `.unity`/`.prefab` gốc đã khôi phục. Ví dụ REF04 được
lấy từ `002_UnityDataAssetPack_datapack__file025`, root pathID 1024.

## Bước kiểm kê gốc (không cần ảnh chụp màn hình)

Từ CMD thư mục repo, với XAPK được sử dụng hợp pháp:

```cmd
git status --short
py -3 tools\audit_original_unity_graph.py
```

Hoặc dùng `CHUAN_BI_DO_HOA.bat` (phần báo cáo sẽ có `Original Unity source graph: PASS`).

Công cụ mở **các serialized Unity file thực tế trong AssetBundle**, đối chiếu:
- GameObject `m_Component`, ID và tên thật, trạng thái active khi đọc được;
- RectTransform `m_GameObject`, `m_Father`, `m_Children` theo PathID;
- anchor, pivot, sizeDelta, anchoredPosition, localScale, full quaternion
  **khi Unity asset cho phép đọc**; không thay thế bằng tọa độ ảnh;
- danh sách type ID từng component, `m_Enabled`, con trỏ `m_Script` và trạng thái
  managed typetree (đọc được hay đã bị IL2CPP loại bỏ);
- dấu vết `Prefab`, `PrefabInstance`, `SceneAsset` và prefab pointer ở đúng
  serialized file; **không kết luận từ tên GameObject giống Prefab**.

**Kết quả duy nhất trên máy:** `output/original-unity-graph.json` (Git ignored).
Không xuất binary, managed script nguồn, texture hoặc XAPK lên GitHub. CI chạy
cùng bộ kiểm tra trên XAPK nhưng chỉ in số liệu tổng hợp, không upload artifact.

## Thử tạo Prefab/Scene từ cây serialized thật (không bịa UI)

Trong Unity Editor:
**Tools → HaiTac Offline UI Viewer → Source XAPK →
Build evidence-only serialized graph prefabs**

Kết quả riêng, không ghi đè REF01–REF04 hiện tại:
- `Assets/LocalReconstruction/SourceGraphPrefabs/REFxx_SERIALIZED_GRAPH.prefab`
- `Assets/LocalReconstruction/SourceGraphScenes/REFxx_SERIALIZED_GRAPH.unity`

Mỗi GameObject trong Prefab có `OriginalSerializedEvidence` chứa PathID thật,
SerializedFile, ID component và trạng thái trường gốc (bao gồm thiếu typetree).
RectTransform được dựng từ các trường serialized đọc được và thứ tự con `m_Children`.

**Cố ý KHÔNG tạo** Image, Camera, CanvasScaler, UI layout, Mask, event,
Text và SkeletonGraphic khi không xác minh được fields/connections. Những
scene nghiên cứu này **không dùng để chơi và có thể hoàn toàn không hiện UI**.
Đây là bản dựng lại *cấu trúc* dựa trên bằng chứng, **không phải Prefab/Scene
nguồn của tác giả game**.

## Điều kiện mới được nhận là Prefab/Scene UI nguyên gốc

1. Chỉ rõ asset/class ID hoặc quan hệ AssetBundle container, đường dẫn scene
   được định danh từ serialized binary. Serialized GameObject riêng lẻ **không
   đủ** để chứng minh nó là Prefab gốc.
2. Khôi phục từng native component và managed MonoBehaviour với đầy đủ
   typetree/field/PPtr; đối chiếu với script runtime thực sự.
3. Khôi phục CanvasScaler, Mask, LayoutGroup, Image 9-slice/Fill, sorting,
   Text/font, Sprite/Atlas, Spine skeleton/skin/animation và trạng thái dữ liệu.
4. Test thực tế trên Unity Editor (Scene/Game/Play Mode), và nếu có quyền
   truy cập game gốc, đối chiếu bằng ảnh/animation **cùng trạng thái và tỷ lệ**.
5. Thành phần thiếu dữ liệu phải xuất báo cáo `UNVERIFIED`, không ghi sai
   rồi tuyên bố 100%.

Một XAPK của game build bằng IL2CPP thường không chứa các file source
`.cs` hoặc đầy đủ file project Editor `.prefab`, `.unity`. Có thể
khôi phục được GameObject serialized và đồ họa nhưng **không cam kết
pixel-perfect 100% chỉ từ XAPK**. Dữ liệu tải qua server hoặc runtime
player không thể tự suy ra.

## Kiểm thử và an toàn

- `python -m unittest discover -s tests -p test_original_unity_graph.py -v`
- CI `local-art-decode.yml` kiểm tra 5 cây và đủ 1.670 Transform trên Linux/Windows,
  **không** khẳng định 1.670 Transform là 1.670 prefab gốc.
- Runtime Spine-Unity hợp pháp và tài nguyên game chỉ nằm local; không đưa
  vào mã nguồn công khai.
- Bộ dựng Canvas preview cũ được giữ tách riêng để so sánh, nhưng phải dán
  nhãn `PROVISIONAL`. Không dùng nó làm chuẩn UI gốc.

**Sau kiểm kê:** đọc `output/original-unity-graph.json`, phân nhóm
`fieldStatus`/component thiếu và source provenance; lên PR riêng cho từng
loại component **chỉ khi có dữ liệu có thể tái tạo**, tránh tạo khung giả.
