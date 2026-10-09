@echo off
setlocal
chcp 65001 >nul
title HaiTac - Offline Web UI Viewer
cd /d "%~dp0"
if not exist "%~dp0unity-ui-viewer\Assets\StreamingAssets\ui-scenes.json" (
    echo [ERROR] Khong tim thay ui-scenes.json.
    echo Chay: py tools\build_unity_viewer_data.py --repo-root .
    pause
    exit /b 1
)
set "PY_CMD="
where py >nul 2>nul
if not errorlevel 1 set "PY_CMD=py -3"
if not defined PY_CMD (
    where python >nul 2>nul
    if not errorlevel 1 set "PY_CMD=python"
)
if not defined PY_CMD (
    echo [ERROR] Chua cai Python 3 hoac chua them Python vao PATH.
    pause
    exit /b 1
)
if not exist "%~dp0output\local-ui-art\manifest.json" (
    echo [INFO] Chua co anh UI giai ma o may. Kiem tra UnityPy...
    %PY_CMD% -c "import UnityPy; import PIL" >nul 2>nul
    if errorlevel 1 (
        echo [INFO] Cai mot lan: %PY_CMD% -m pip install UnityPy Pillow
        echo [INFO] Neu XAPK la LFS pointer: git lfs pull
    ) else (
        echo [INFO] Dang doc XAPK/APK va trich xuat Sprite. Lan dau co the mat vai phut...
        %PY_CMD% "%~dp0tools\export_local_ui_art.py"
        if errorlevel 1 echo [WARN] Chua trich xuat duoc anh. UI Viewer van chay voi wireframe.
    )
)
if not exist "%~dp0output\local-spine\manifest.json" (
    %PY_CMD% -c "import UnityPy; import PIL" >nul 2>nul
    if not errorlevel 1 (
        echo [INFO] Dang tao cac bo Spine skeleton, atlas, texture tai may...
        %PY_CMD% "%~dp0tools\export_local_spine.py" --limit 12
        if errorlevel 1 echo [WARN] Chua tao duoc Spine pack. Xem CHUAN_BI_DO_HOA.bat.
    )
)
%PY_CMD% "%~dp0tools\serve_ui_viewer.py"
echo.
echo Nhan phim bat ky de dong.
pause >nul
endlocal
