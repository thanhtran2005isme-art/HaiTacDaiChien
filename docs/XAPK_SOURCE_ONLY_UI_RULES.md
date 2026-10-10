# QUY TẮC BẮT BUỘC: UI chỉ từ XAPK gốc — KHÔNG ĐOÁN

**Phạm vi:** toàn bộ việc phân tích, xuất Sprite, phục dựng Canvas/HUD/RectTransform/LayoutGroup/Text/Animation, kiểm thử và sửa UI game Hải Tặc Đại Chiến; hiện ưu tiên **REF04 UI/icon**, **nhân vật Spine để sau**.

## Quy tắc tuyệt đối

1. **Nguồn sự thật duy nhất cho dữ liệu game là XAPK gốc được người dùng cung cấp/cấp phép** (APK bên trong, Unity AssetBundle/SerializedFile, Texture/SpriteAtlas, IL2CPP binary + metadata và những giá trị runtime được truy vết bằng chứng tới mã game gốc). Không lấy ảnh mẫu web, game khác, tên asset hoặc mô tả do AI suy luận làm dữ liệu gốc.
2. **KHÔNG BAO GIỜ tự phỏng đoán** vị trí/tọa độ, anchor, pivot, kích thước, scale, rotation, sibling order, Canvas/CanvasScaler, reference resolution, safe area, HUD, trạng thái active/enabled, thứ tự render, Text/font, Sprite/atlas/border/PPU/offset, LayoutGroup, animation hay logic runtime. **KHÔNG** thay bằng default của Unity rồi gọi nó là “đúng XAPK”.
3. Mỗi giá trị đưa vào sản phẩm phải truy xuất được **SerializedFile + PathID/GameObject ID/Component ID/field gốc + phương pháp giải mã**, hoặc cặp offset/pointer/giá trị trong IL2CPP đã đối chiếu byte thực, hash nguồn, trạng thái kiểm chứng và phạm vi hiệu lực. Các giá trị runtime phải chứng minh được điều kiện kích hoạt và phép tính, không tự giả định dữ liệu máy hoặc tài khoản.
4. **Nếu không có giá trị trong báo cáo hiện tại:** ưu tiên giải mã tiếp dữ liệu **XAPK thật**, mở rộng phạm vi tìm Prefab/Scene/ancestor, phục hồi managed TypeTree và kiểm tra IL2CPP. Chỉ áp dụng sau khi kiểm tra dữ liệu đầy đủ và nhất quán. **Nếu giải mã vẫn không lấy được thì DỪNG điểm đó và HỎI NGƯỜI DÙNG**, nêu đúng field/asset/bằng chứng còn thiếu; không tạo placeholder hoặc chọn số gần đúng.
5. Trường hợp chỉ có một decoder/backend đọc được thì **không tự nâng thành chứng cứ đủ để áp dụng**. Cần phép kiểm độc lập (đối chiếu object bytes/schema/owner/offset) hoặc xác nhận rõ ràng từ người dùng sau khi báo thiếu dữ liệu. Một giá trị chưa đủ chứng minh được đánh dấu `UNVERIFIED` / `BLOCKED`, không đoán.
6. Ảnh PNG có kích thước khác `Sprite.m_Rect` phải kiểm tra `m_RD.textureRect`, `textureRectOffset`, packing/mesh/atlas và byte pixel XAPK. Không kéo giãn, dịch, thêm/đoán pixel tùy ý. Mọi phiên bản `PREVIEW` là **tách biệt**, không được coi là bản gốc hoặc cho vào UI cuối khi chưa kiểm chứng.
7. `PASS` từ Python, GitHub CI, JSON audit hay Unity Editor chỉ chứng minh **chính điều đã được kiểm tra**. Không tuyên bố **UI giống XAPK**, pixel-perfect hay “hoàn thành 100%” khi chưa khôi phục và đối chiếu runtime đúng trạng thái.
8. **Không tự dựng hoặc sửa Canvas 1600×900, root scale=1, camera preview, layout, một icon vị trí khác**, kể cả để “cho đẹp”, rồi trình bày là sản phẩm. Thử nghiệm cũ chỉ được giữ ở vùng `*_PREVIEW/*_STUDY`, có nhãn không phải dữ liệu XAPK; tuyệt đối không nhập giá trị preview vào sản phẩm cuối.
9. **Không chỉnh 3C verified Prefab, PNG xuất nguồn, hoặc file dữ liệu XAPK để hợp thức hóa UI**. Công cụ đọc chứng cứ ở `output/` (gitignored); phép khôi phục có chứng cứ phải xuất riêng, kiểm toán và rollback an toàn.
10. **Nếu còn thiếu dữ liệu quan trọng, báo cáo thiếu và hỏi** — người dùng không có nghĩa vụ chụp màn hình để thay cho việc đọc những field đã có trong XAPK. Ảnh runtime chính hãng chỉ dành cho **bước nghiệm thu đối chiếu cuối cùng**, không thay thế việc truy xuất tọa độ từ source.

## Thứ tự thực hiện đã được người dùng khóa

1. **Kiểm kê TẤT CẢ dữ liệu UI trong REF04 từ XAPK:** mọi GameObject, RectTransform, thứ tự con, trạng thái active, native component, toàn bộ MonoBehaviour/MonoScript, Image (kể cả không Sprite), Mask, Canvas, CanvasScaler, LayoutGroup, text/custom UI; đối chiếu graph/IL2CPP/Sprite PathID. Phân loại chính xác `SOURCE_VERIFIED`, `PARTIALLY_VERIFIED`, `UNVERIFIED`, `BLOCKED` theo từng field. Xuất báo cáo local `output/ref04-full-source-inventory.json`. **Bước 1 chỉ đọc, không thay Unity.**
2. Khôi phục component và quy tắc layout còn thiếu CHỈ KHI chứng minh được trực tiếp từ XAPK; trạng thái thiếu phải `BLOCKED`.
3. Truy dấu mã **IL2CPP REF04** để xác định các phép tính runtime và điều kiện áp dụng tọa độ, kích thước, visibility, text; không suy luận từ string metadata.
4. Dựng UI REF04 theo những giá trị nguồn đã xác minh, không đoán Canvas/viewport hoặc tự đặt icon.
5. Cuối cùng đối chiếu Game View với game XAPK thật ở cùng trạng thái. Chưa có ảnh đối chiếu không cho phép tuyên bố “giống y đúc”.

## Báo cáo và điều kiện dừng

Mỗi lượt phải ghi: `evidence-source`, `sourceFile`, `componentPathId`, `gameObjectPathId`, `field`, `verificationStatus`, `blockingReason`, `nextDecodeAction` khi áp dụng. **Không thay các giá trị `UNKNOWN` bằng số giả**, kể cả giá trị `0` hoặc `1600x900`. Sau bước 1, trình bày bảng dữ liệu đã xác minh và danh sách chỗ phải giải mã thêm. Nếu không thể giải mã tiếp bằng phương tiện hiện có, hỏi người dùng đúng dữ liệu/quyền truy cập cần thiết, không lặng lẽ tạo UI khác.

**Chỉ dẫn ưu tiên cao cho agent:** Nếu yêu cầu “làm giống XAPK 100%” xung đột với việc thiếu nguồn, **dừng và báo thiếu** thay vì tạo tác giả tưởng. Tài liệu này là quy tắc người dùng, không được nới lỏng bởi suy đoán về “layout hợp lý”.
