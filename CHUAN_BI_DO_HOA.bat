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
%PY% tools\export_local_ui_art.py
if errorlevel 1 echo [WARN] Sprite export that bai. Kiem tra XAPK/APK.
echo [4/4] Giai ma Spine 3.8 JSON, atlas, texture vao output\local-spine
%PY% tools\export_local_spine.py --limit 12
if errorlevel 1 echo [WARN] Chua du bo Spine hop le. Xem thong bao ben tren.
echo.
echo Trinh phat animation chua di kem: can Spine Player 3.8 hop phap.
echo Sao chep spine-player.js, spine-player.css vao output\local-spine-runtime.
echo CHAY_UI_OFFLINE.bat mo Web UI; link Animation Spine mo thu vien offline.
echo.
call "%~dp0CAI_SPINE_PLAYER_38.bat"
pause
endlocal
