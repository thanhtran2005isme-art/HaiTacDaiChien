@echo off
setlocal
chcp 65001 >nul
title HaiTac - Chuan bi do hoa UI va Spine local
cd /d "%~dp0"
where py >nul 2>nul
if not errorlevel 1 (
    set "PY=py -3"
) else (
    where python >nul 2>nul
    if errorlevel 1 (
        echo [ERROR] Can cai Python 3 va them vao PATH.
        pause
        exit /b 1
    )
    set "PY=python"
)
echo [1/4] Lay XAPK that tu Git LFS. Can mang, co the phai tai hon 300 MB.
where git >nul 2>nul
if errorlevel 1 (
    echo [WARN] Khong tim thay git. Hay tu dat XAPK hop le vao thu muc du an.
) else (
    git lfs pull
    if errorlevel 1 echo [WARN] Git LFS khong lay duoc XAPK. Kiem tra mang/quyen truy cap.
)
echo [2/4] Cai UnityPy va Pillow vao Python dang dung.
%PY% -m pip install UnityPy Pillow
if errorlevel 1 (
    echo [ERROR] Khong cai duoc thu vien. Kiem tra pip va ket noi Internet.
    pause
    exit /b 1
)
echo [3/4] Giai ma Sprite UI vao output\local-ui-art
set "SPRITE_STATE=FAILED"
%PY% tools\export_local_ui_art.py
if not errorlevel 1 set "SPRITE_STATE=PASS"
if /I "%SPRITE_STATE%"=="FAILED" (
    echo [ERROR] Chua tao duoc Sprite UI. Xem output\local-ui-art\export-status.json.
    echo [ERROR] Khi mo web, local-art/manifest.json se khong co.
)
if /I "%SPRITE_STATE%"=="PASS" (
    echo [INFO] Tao du lieu Canvas/Prefab cho Unity Editor...
    %PY% tools\build_unity_prefab_manifest.py
    if errorlevel 1 echo [WARN] Chua tao duoc Unity reconstruction plan.
)
echo [INFO] Trich xuat bo cuc goc duoc xac minh tu XAPK...
set "LAYOUT_STATE=FAILED"
%PY% tools\export_local_ui_layout.py
if not errorlevel 1 set "LAYOUT_STATE=PASS"
if /I "%LAYOUT_STATE%"=="FAILED" (
    echo [WARN] Khong doc duoc bo cuc goc. Unity tiep tuc dung du lieu cu.
    echo [WARN] Thu chay: %PY% tools\export_local_ui_layout.py
)
set "SOURCE_IMAGE_STATE=FAILED"
if /I "%SPRITE_STATE%"=="PASS" if /I "%LAYOUT_STATE%"=="PASS" (
    %PY% -c "import json; from pathlib import Path; a=json.loads(Path('output/local-ui-art/manifest.json').read_text(encoding='utf-8')); b=json.loads(Path('output/local-ui-layout.json').read_text(encoding='utf-8')); p=json.loads(Path('output/unity-prefab-map.json').read_text(encoding='utf-8')); assert b['version']==2 and p['schemaVersion']==2; assert len(a['nodeBindings'])==len(p['sprites'])==b['stats']['exactImageBindings']; print('Original Image pointers:',len(a['nodeBindings']),'Disabled original Images:',b['stats']['disabledImagesByScene'])"
    if not errorlevel 1 set "SOURCE_IMAGE_STATE=PASS"
)
if /I "%SOURCE_IMAGE_STATE%"=="FAILED" (
    echo [WARN] Can rebuild Sprite/Image owner links. Khong coi preview cu la UI goc.
)
echo [INFO] Trich xuat GameObject/Component goc tu serialized Unity Assets...
set "SOURCE_GRAPH_STATE=FAILED"
if /I "%LAYOUT_STATE%"=="PASS" (
    %PY% tools\audit_original_unity_graph.py
    if not errorlevel 1 set "SOURCE_GRAPH_STATE=PASS"
)
if /I "%SOURCE_GRAPH_STATE%"=="FAILED" (
    echo [WARN] Chua kiem chung duoc Scene/Prefab goc, khong duoc doan.
) else (
    echo [INFO] Tao danh sach component con thieu theo ID goc...
    %PY% tools\summarize_original_unity_gaps.py
    if errorlevel 1 echo [WARN] Danh sach component con thieu chua tao duoc.
)
set "IL2CPP_UI_PROVENANCE=FAILED"
if /I "%SOURCE_GRAPH_STATE%"=="PASS" (
    echo [INFO] Doi chieu MonoScript - MonoBehaviour - metadata IL2CPP theo ID goc...
    %PY% tools\decode_xapk_ui_provenance.py
    if not errorlevel 1 set "IL2CPP_UI_PROVENANCE=PASS"
)
if /I "%IL2CPP_UI_PROVENANCE%"=="FAILED" echo [WARN] Chua du bang chung typetree / MonoScript. Khong tu giai ma offsets.
echo [INFO] Kiem ke Sprite border, CanvasScaler, Mask, LayoutGroup tu XAPK...
set "UI_DEEP_STATE=FAILED"
%PY% tools\audit_local_ui_components.py
if not errorlevel 1 set "UI_DEEP_STATE=PASS"
if /I "%UI_DEEP_STATE%"=="FAILED" (
    echo [WARN] XAPK UI deep evidence khong du. Khong thay the du lieu goc bang gia lap.
)
echo [INFO] Truy nguyen con tro Spine va TextAsset theo GameObject...
set "SPINE_LINK_STATE=FAILED"
%PY% tools\trace_local_spine_links.py
if not errorlevel 1 set "SPINE_LINK_STATE=PASS"
if /I "%SPINE_LINK_STATE%"=="FAILED" (
    echo [WARN] Chua xac minh duoc chuoi Spine; khong tu chon nhan vat hay animation.
)
echo [4/4] Giai ma Spine 3.8 theo dung lien ket REF04 da xac minh...
set "SPINE_STATE=FAILED"
if /I "%SPINE_LINK_STATE%"=="PASS" (
    %PY% tools\export_local_spine.py --from-evidence REF04-home-crew --limit 12
    if not errorlevel 1 set "SPINE_STATE=PASS"
) else (
    echo [WARN] Chua co du bang chung de chon pack REF04. Khong tu doan nhan vat.
)
if /I "%SPINE_STATE%"=="FAILED" echo [WARN] Chua du bo Spine hop le. Xem thong bao ben tren.
echo.
echo Trinh phat animation chua di kem: can Spine Player 3.8 hop phap.
echo Sao chep spine-player.js, spine-player.css vao output\local-spine-runtime.
echo CHAY_UI_OFFLINE.bat mo Web UI; link Animation Spine mo thu vien offline.
echo.
if not exist "%~dp0output\local-spine-runtime\spine-player.js" (
    call "%~dp0CAI_SPINE_PLAYER_38.bat"
) else (
    echo [OK] Spine Player 3.8 da co tren may.
)
echo.
echo ======================= KET QUA =======================
echo Sprite UI: %SPRITE_STATE%
echo Unity verified layout: %LAYOUT_STATE%
echo Original Image owner/state: %SOURCE_IMAGE_STATE%
echo Original Unity source graph: %SOURCE_GRAPH_STATE%
echo IL2CPP UI source provenance: %IL2CPP_UI_PROVENANCE%
echo UI component evidence: %UI_DEEP_STATE%
echo Spine reference evidence: %SPINE_LINK_STATE%
echo Spine pack: %SPINE_STATE%
echo Spine Player: can kiem tra tai trang /spine-viewer
echo =======================================================
if /I "%SPRITE_STATE%"=="FAILED" (
    echo [ERROR] Cac khung UI van se la wireframe cho den khi Sprite export PASS.
    echo [ERROR] Gui noi dung output\local-ui-art\export-status.json de kiem tra.
)
pause
endlocal
