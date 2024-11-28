@echo off
setlocal enabledelayedexpansion

set "folder=%~dp0"

pushd "%folder%"

for %%f in (*.txt) do (
    echo Converted: %%f
    "STBLize.exe" "%%f"
)

popd
pause
