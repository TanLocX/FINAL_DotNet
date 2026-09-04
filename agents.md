# Project-Specific Agent Instructions: PNJ Jewelry Manager

> This document outlines operational instructions, toolchains, and constraints specific to the PNJ Jewelry Store Management project.

---

## 1. Environment & Build Toolchain
- **Platform:** Microsoft .NET Framework 4.7.2 (Windows Forms)
- **Language:** C# 7.3
- **Solution / Project File:** `FINAL_DotNet\FINAL_DotNet.csproj`
- **MSBuild Compiler Path:** `D:\Program Files (x86)\VisualStudio\MSBuild\Current\Bin\MSBuild.exe`
- **Build Command:**
  ```powershell
  & "D:\Program Files (x86)\VisualStudio\MSBuild\Current\Bin\MSBuild.exe" "FINAL_DotNet\FINAL_DotNet.csproj" /t:Rebuild /p:Configuration=Debug /v:m
  ```
- **Constraint:** Ensure 0 Warnings, 0 Errors upon rebuilding.

---

## 2. Database Constraints
- **Engine:** Microsoft SQL Server (LocalDB `(localdb)\MSSQLLocalDB` or `.\SQLEXPRESS`)
- **Database Name:** `QL_CuaHangDaQuy_PNJ`
- **Physical Backup:** `Database\QL_CuaHangDaQuy_PNJ.bak` (6.4 MB)
- **ORM:** Entity Framework 6.4.4 Database First via `Model1.edmx`. Do NOT modify EDMX generated code directly; update partial classes or service layers.

---

## 3. Critical Code & Asset Policies
- **English-Only in Code:** Variable names, method names, class names, constants, and commit messages MUST be strictly in English.
- **Image Optimization:** Never ingest uncompressed raw assets into `Resources/`. Always route external images through `ImageOptimizationHelper.SaveOptimizedProductImage(...)` to preserve memory and disk space.
- **Git Policy:** STRICTLY LOCAL COMMITS ONLY. Never execute `git push` to remote repositories.

---

## 4. Key Services & Helpers
- `PosService.cs`: Point-of-Sale calculations, stock transactions, and invoice issuance.
- `ImageOptimizationHelper.cs`: High-Quality Bicubic downscaling and asset synchronization.
- `SaoLuuPhucHoiService.cs`: Adaptive database backup and restore with `COMPRESSION` fallback.
- `BaoCaoService.cs`: Invoice and warranty report generation.
- `CurrentUserSession.cs`: Thread-safe session singleton.