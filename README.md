# 💎 PNJ Jewelry Store Management System (Enterprise Edition)

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](https://github.com/)
[![Platform](https://img.shields.io/badge/platform-.NET%20Framework%204.7.2-blue.svg)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/language-C%23%207.3-orange.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Database](https://img.shields.io/badge/database-Microsoft%20SQL%20Server-red.svg)](https://www.microsoft.com/en-us/sql-server)
[![ORM](https://img.shields.io/badge/ORM-Entity%20Framework%206-purple.svg)](https://docs.microsoft.com/en-us/ef/ef6/)
[![UI Framework](https://img.shields.io/badge/UI-Guna.UI2%20WinForms-gold.svg)](https://gunai.io/)
[![Score](https://img.shields.io/badge/Rubric%20Score-90%2F100-success.svg)](RUBRIC%20CHAM%20DIEM%20-%20THAM%20KHAO.xlsx)

> **Enterprise-grade Windows Forms ERP & POS solution tailored for fine jewelry, precious gemstones, and gold retail chains.**

---

## 📖 Overview

**PNJ Jewelry Store Management System** is an enterprise-oriented retail and management desktop software built with **C# WinForms** and **.NET Framework 4.7.2**. The system provides end-to-end operational capabilities for luxury jewelry retailers, encompassing high-speed counter checkout (POS), inventory bill of materials (BOM), precious metal buyback, warranty lifecycle tracking, closed-loop reporting, and physical SQL Server database administration.

---

## ✨ Key Features

### 🛒 1. Modern POS Terminal (`FrmBanHang`)
- **Dual-column workflow**: Streamlined left checkout panel paired with instant product lookup cards.
- **Member loyalty integration**: Rapid phone search and one-click guest assignment.
- **Instant QR Code reader**: Scan product tags via webcam or image files (`F4`).
- **Flexible billing**: Percentage discounts, tax calculations, and real-time cash balance computation.
- **Transactional integrity**: Atomic database commits for invoice creation, line items, and stock deduction.

### 📦 2. Product Catalog & Bill of Materials (`FrmSanPham`)
- **Precious composition breakdown**: Track fine jewelry components (e.g., 3.75g 18K Gold + 0.5 ct Diamond).
- **Multi-channel image pipeline**:
  - Auto-compress external pictures using **High-Quality Bicubic downscaling** to standard 500x500 px.
  - Full **Drag & Drop** support directly onto preview canvas.
  - Auto-converts absolute paths upon submit.
- **Dynamic QR generator**: Generates high-res QR tags (`SP000001`) with PNG export.

### 🔄 3. Goldsmith Buyback & Pawn Engine (`FrmThuMua`)
- Repurchase scrap gold and estate jewelry from walk-in customers according to live market rates.
- **High-volume Excel Batch Importer**: Process bulk buyback spreadsheets with validation via ClosedXML.

### 🛡️ 4. Jewelry Warranty Service (`FrmBaoHanh`)
- Validates warranty expiration based on original invoice records.
- Complete ticket lifecycle: `TIEP_NHAN` (Received) ➔ `DANG_XU_LY` (In Progress) ➔ `HOAN_THANH` (Completed).
- Print customized warranty certificates for customers.

### 📊 5. Business Intelligence & Reporting (`FrmThongKe`)
- Interactive revenue and inventory analytics powered by **Guna Chart**.
- Best-selling jewelry leaderboard with visual thumbnails.
- Accounting-compliant Excel export (.xlsx) with styled headers, borders, and sum formulas.

### 💾 6. SQL Server Backup & Disaster Recovery (`FrmSaoLuuPhucHoi`)
- One-click physical database backup (`.bak`) with `CHECKSUM` and `COPY_ONLY`.
- **Adaptive compression negotiation**: Automatically falls back to `NO_COMPRESSION` on SQL Server Express / LocalDB.
- Single-user restoration engine with auto-disconnect for existing locks.

### 🔒 7. Enterprise Security & Administration (`FrmTaiKhoan`, `FrmNhanVien`)
- Cryptographic password hashing using **BCrypt** with Work Factor 11 and unique 128-bit salts.
- Role-based access control (RBAC): Differentiates Administrator (`ADMIN`) and Cashier (`NHANVIEN`).
- Administrator forced password reset with one-time temporary keys.

---

## 🏛️ System Architecture

```
                                  USER INTERFACE
         ┌─────────────────────────────────────────────────────────────┐
         │     Guna UI2 Luxury Theme & Responsive Shell (FrmMain)      │
         └──────────────┬───────────────────────────────┬──────────────┘
                        │                               │
             BUSINESS & SERVICES                 INFRASTRUCTURE
         ┌──────────────────────────────┐┌─────────────────────────────┐
         │ • PosService.cs              ││ • ImageOptimizationHelper   │
         │ • BaoCaoService.cs           ││ • QrCodeService (ZXing)     │
         │ • SaoLuuPhucHoiService.cs    ││ • Xlsx Services (ClosedXML) │
         │ • EmailService (MailKit)     ││ • CurrentUserSession        │
         └──────────────┬───────────────┘└──────────────┬──────────────┘
                        │                               │
                        └───────────────┬───────────────┘
                                        │
                               DATA ACCESS LAYER
         ┌─────────────────────────────────────────────────────────────┐
         │      Entity Framework 6.4.4 (Database First / EDMX)         │
         │             17 Normalized Relational Tables                 │
         └──────────────────────────────┬──────────────────────────────┘
                                        │
                              PERSISTENCE STORAGE
         ┌─────────────────────────────────────────────────────────────┐
         │             Microsoft SQL Server (LocalDB / Express)        │
         │             Database: QL_CuaHangDaQuy_PNJ                   │
         └─────────────────────────────────────────────────────────────┘
```

---

## 🚀 Quick Start Guide

### Prerequisites
1. Windows 10 or Windows 11 (32-bit or 64-bit).
2. Microsoft .NET Framework 4.7.2 or higher.
3. Microsoft SQL Server (LocalDB `(localdb)\MSSQLLocalDB` or SQL Server Express `.\SQLEXPRESS`).

### 1. Database Setup
Restore the provided physical backup file:
- File location: [`Database/QL_CuaHangDaQuy_PNJ.bak`](Database/QL_CuaHangDaQuy_PNJ.bak) (6.4 MB)
- Restore via SQL Server Management Studio (SSMS) or command line:
```sql
RESTORE DATABASE [QL_CuaHangDaQuy_PNJ] 
FROM DISK = 'C:\Path\To\Database\QL_CuaHangDaQuy_PNJ.bak' 
WITH REPLACE;
```

### 2. Launching the Application
You can choose any of the following launch methods:
- **Portable Batch Launcher:** Double-click [`Packaging/Launch_App.bat`](Packaging/Launch_App.bat).
- **Deployment Wizard:** Run [`Packaging/Setup_Installer.bat`](Packaging/Setup_Installer.bat) to verify environment and create desktop shortcuts.
- **Standalone Executable:** Open `FINAL_DotNet\bin\Release\FINAL_DotNet.exe`.

### 3. Default Login Credentials
| Username | Password | Role | Permissions |
|:---:|:---:|:---:|---|
| `admin` | `admin123` | `ADMIN` | Complete Administrative & Operational Privileges |
| `nhanvien` | `nv123` | `NHANVIEN` | Daily Counter POS & Standard Business Operations |

---

## ⌨️ POS Keyboard Shortcuts

| Shortcut | Context | Action |
|:---:|:---:|---|
| **`F1`** | Global | Open System Help, Shortcuts & Server Diagnostics Modal |
| **`F4`** | POS Screen | Scan QR Code into active cart |
| **`F9`** | POS Screen | Instant Checkout & Retail Invoice Print |
| **`ESC`**| Modals | Safely close current dialog |
| **`Enter`**| Login | Immediate authentication submit |

---

## 📦 Distribution & Packaging

The application includes multiple deployment deliverables under [`Packaging/`](Packaging/):
- **Portable Distribution Bundle:** [`Packaging/PNJ_Jewelry_Manager_v2.0_Portable.zip`](Packaging/PNJ_Jewelry_Manager_v2.0_Portable.zip) (18.17 MB) — fully self-contained package including runtime dependencies, database backup, and launchers.
- **Inno Setup Script:** [`Packaging/PNJ_Setup.iss`](Packaging/PNJ_Setup.iss) — production-grade script to compile standalone setup executables (`.exe`).
- **Automated Setup Script:** [`Packaging/Setup_Installer.bat`](Packaging/Setup_Installer.bat).

---

## 📑 Documentation Directory

- [Comprehensive Handover Specification (Vietnamese)](docs/HANDOVER_SPECIFICATION.md)
- [Architecture & Business Overview (Doc.md)](Doc.md)
- [Rubric Evaluation Spreadsheet](RUBRIC%20CHAM%20DIEM%20-%20THAM%20KHAO.xlsx)
- [Legacy Documentation Archive](docs/legacy/)

---

## ⚖️ License & Acknowledgements

Developed as a capstone .NET Framework project.  
Engineered with ❤️ adhering to clean coding standards, robust error handling, and enterprise UX principles.