# Tài liệu dự án Hải Tặc

**Lần sau AI đọc:** [AGENTS.md](../AGENTS.md) → [AI_HANDOFF.md](AI_HANDOFF.md) → [ARCHITECTURE.md](ARCHITECTURE.md). Không cần nạp toàn bộ lịch sử.

| File | Mục đích |
|---|---|
| [XAPK_DEEP_UI_FIELD_PROVENANCE.md](XAPK_DEEP_UI_FIELD_PROVENANCE.md) | Giai đoạn giải mã sâu: MonoScript PPtr, IL2CPP v31 và field UI gốc, không đoán layout |
| [XAPK_IMAGE_RENDER_CORRECTION.md](XAPK_IMAGE_RENDER_CORRECTION.md) | Sửa Image theo ID nguồn, tôn trọng m_Enabled và ghi rõ phần còn thiếu |
| [XAPK_NATIVE_SCENE_PREFAB_RECOVERY.md](XAPK_NATIVE_SCENE_PREFAB_RECOVERY.md) | Kiểm kê Serialized GameObject, Prefab/Scene và giới hạn bản khôi phục thật |
| [AI_HANDOFF.md](AI_HANDOFF.md) | Tình trạng mới nhất, việc đã làm, việc còn tồn đọng và lệnh chạy |
| [ARCHITECTURE.md](ARCHITECTURE.md) | Cấu trúc repo, kiến trúc dữ liệu và các môi trường |
| [DECISIONS.md](DECISIONS.md) | Vì sao chọn các phương án kỹ thuật, những điều không được suy đoán |
| [TROUBLESHOOTING.md](TROUBLESHOOTING.md) | Lỗi đã gặp, cách xác định và sửa |
| [WORKFLOW.md](WORKFLOW.md) | Quy trình branch → PR → kiểm thử → merge `main` |
| [history/2026-10.md](history/2026-10.md) | Các commit gần đây và tổng kết theo tháng |
| [SCREEN_REFERENCE_MATCHING.md](SCREEN_REFERENCE_MATCHING.md) | Ánh xạ 5 scene tham chiếu từ XAPK |
| [RUNTIME_SCREEN_CHECKLIST.md](RUNTIME_SCREEN_CHECKLIST.md) | Kiểm thử giao diện của bản chạy thật |
| [PRIVATE_VISUAL_VERIFICATION.md](PRIVATE_VISUAL_VERIFICATION.md) | Đối chiếu ảnh offline được phép sử dụng |

**Nguyên tắc:** Handoff phải ngắn; không sao chép nguyên log dài vào docs; dùng link commit/PR khi cần chứng cứ. Với lịch sử đầy đủ, `git log --oneline --decorate` hoặc tab Commits của GitHub mới là nguồn chính.
