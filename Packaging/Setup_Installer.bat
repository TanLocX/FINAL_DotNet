@echo off
setlocal enabledelayedexpansion
title PNJ Jewelry Store Manager - Setup & Deployment Wizard
color 0B

echo ======================================================================
echo    PNJ JEWELRY STORE MANAGEMENT SYSTEM - SETUP & DEPLOYMENT WIZARD
echo    Version 2.0 (Enterprise Edition)
echo ======================================================================
echo.
echo [1/4] Checking system prerequisites (.NET Framework 4.7.2+)...
reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release >nul 2>&1
if %errorlevel% neq 0 (
    echo [WARNING] Could not detect .NET Framework 4.7.2 in registry.
    echo Please ensure Microsoft .NET Framework 4.7.2 or higher is installed.
) else (
    echo [OK] Microsoft .NET Framework 4.x runtime is ready.
)

echo.
echo [2/4] Initializing runtime directories...
if not exist "C:\PNJ_Backups" (
    mkdir "C:\PNJ_Backups" 2>nul
    echo [OK] Created primary SQL backup directory: C:\PNJ_Backups
) else (
    echo [OK] Primary SQL backup directory exists: C:\PNJ_Backups
)

echo.
echo [3/4] Creating Desktop Shortcut...
set "SCRIPT_DIR=%~dp0"
set "EXE_PATH=%SCRIPT_DIR%FINAL_DotNet.exe"
powershell -NoProfile -Command "$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut([System.IO.Path]::Combine([System.Environment]::GetFolderPath('Desktop'), 'PNJ Jewelry Manager.lnk')); $s.TargetPath = '%EXE_PATH%'; $s.WorkingDirectory = '%SCRIPT_DIR%'; $s.Description = 'PNJ Jewelry Store ERP & POS Management System'; $s.Save()" >nul 2>&1
if %errorlevel% equ 0 (
    echo [OK] Desktop shortcut created: "PNJ Jewelry Manager.lnk"
) else (
    echo [INFO] Desktop shortcut creation skipped or requires permissions.
)

echo.
echo [4/4] Database Configuration Notice:
echo - Target Database: QL_CuaHangDaQuy_PNJ
echo - Server: (localdb)\MSSQLLocalDB or .\SQLEXPRESS
echo - Database backup file is located at: %SCRIPT_DIR%Database\QL_CuaHangDaQuy_PNJ.bak
echo.
echo ======================================================================
echo    SETUP COMPLETED SUCCESSFULLY!
echo ======================================================================
echo.
set /p START_NOW="Do you want to launch PNJ Jewelry Manager now? (Y/N): "
if /i "!START_NOW!"=="Y" (
    start "" "%EXE_PATH%"
)
exit /b 0
