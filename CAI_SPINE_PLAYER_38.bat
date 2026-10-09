@echo off
setlocal
chcp 65001 >nul
title Cai Spine Player 3.8 (chi tren may)
cd /d "%~dp0"
echo.
echo SPINE PLAYER 3.8 - CHI DUNG NOI BO TREN MAY
echo Day la Spine Runtimes cua Esoteric Software, co dieu kien cap phep.
echo Vui long xem: https://esotericsoftware.com/spine-runtimes-license
echo Can co quyen su dung hop le khi tich hop Spine Runtimes.
echo Khong dua runtime hay tai nguyen game vao GitHub cong khai.
echo.
set "CONFIRM="
set /p "CONFIRM=Ban xac nhan co quyen su dung hop le va dong y tai runtime 3.8? (Y/N): "
if /I not "%CONFIRM%"=="Y" (
    echo [SKIP] Khong tai runtime. Ban van co the xem metadata va Sprite.
    exit /b 0
)
where curl.exe >nul 2>nul
if errorlevel 1 (
    echo [ERROR] Can curl.exe tren Windows hoac tai Spine Player 3.8 tu nguon chinh thuc.
    exit /b 1
)
set "OUT=%~dp0output\local-spine-runtime"
if not exist "%OUT%" mkdir "%OUT%"
set "BASE=https://raw.githubusercontent.com/EsotericSoftware/spine-runtimes/3.8/spine-ts"
echo Tai spine-player.js (nhanh 3.8) qua HTTPS ...
curl.exe --fail --location --retry 2 --silent --show-error --output "%OUT%\spine-player.js.tmp" "%BASE%/build/spine-player.js"
if errorlevel 1 goto failed
echo Tai spine-player.css (nhanh 3.8) qua HTTPS ...
curl.exe --fail --location --retry 2 --silent --show-error --output "%OUT%\spine-player.css.tmp" "%BASE%/player/css/spine-player.css"
if errorlevel 1 goto failed
for %%F in ("%OUT%\spine-player.js.tmp") do if %%~zF LSS 100000 goto failed
for %%F in ("%OUT%\spine-player.css.tmp") do if %%~zF LSS 10000 goto failed
move /Y "%OUT%\spine-player.js.tmp" "%OUT%\spine-player.js" >nul
move /Y "%OUT%\spine-player.css.tmp" "%OUT%\spine-player.css" >nul
echo [PASS] Spine Player 3.8 da duoc chuan bi tai output\local-spine-runtime.
exit /b 0
:failed
del /Q "%OUT%\spine-player.js.tmp" >nul 2>nul
del /Q "%OUT%\spine-player.css.tmp" >nul 2>nul
echo [ERROR] Khong tai duoc runtime. Khong thay doi cac file da co.
exit /b 1
