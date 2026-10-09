@echo off
setlocal
chcp 65001 >nul
title HaiTac - Offline Web UI Viewer
cd /d "%~dp0"
if not exist "%~dp0unity-ui-viewer\Assets\StreamingAssets\ui-scenes.json" (
    echo [ERROR] Khong tim thay ui-scenes.json.
    echo Hay cap nhat GitHub: git pull origin main
    echo Hoac chay: py tools\build_unity_viewer_data.py --repo-root .
    pause
    exit /b 1
)
where py >nul 2>nul
if not errorlevel 1 (
    py -3 "%~dp0tools\serve_ui_viewer.py"
    goto :done
)
where python >nul 2>nul
if not errorlevel 1 (
    python "%~dp0tools\serve_ui_viewer.py"
    goto :done
)
echo [ERROR] Chua cai Python hoac chua them Python vao PATH.
echo Cai Python 3 va thu lai. Khong can cai Unity.
:done
echo.
echo Nhan phim bat ky de dong.
pause >nul
endlocal
