using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FINAL_DotNet
{
    public partial class QL_CuaHangDaQuy_PNJEntities
    {
        // ChiTietChatLieu uses a composite key, so it has no identity to reset.
        private static readonly HashSet<Type> IdentityEntityTypes = new HashSet<Type>
        {
            typeof(NhanVien), typeof(TaiKhoan), typeof(KhachHang), typeof(DanhMuc),
            typeof(ChatLieu), typeof(SanPham), typeof(NhaCungCap), typeof(HoaDon),
            typeof(ChiTietHoaDon), typeof(PhieuNhap), typeof(ChiTietPhieuNhap),
            typeof(PhieuThuMua), typeof(ChiTietPhieuThuMua), typeof(PhieuBaoHanh),
            typeof(MauEmail), typeof(NhatKyGuiEmail)
        };

        public override int SaveChanges()
        {
            string[] tables = GetAddedIdentityTables();
            if (tables.Length == 0) return base.SaveChanges();

            // POS, imports and product creation already own their transactions.
            if (Database.CurrentTransaction != null)
            {
                PrepareIdentityTables(tables);
                return base.SaveChanges();
            }

            using (var transaction = Database.BeginTransaction())
            {
                PrepareIdentityTables(tables);
                int affectedRows = base.SaveChanges();
                transaction.Commit();
                return affectedRows;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            string[] tables = GetAddedIdentityTables();
            if (tables.Length == 0)
                return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (Database.CurrentTransaction != null)
            {
                await PrepareIdentityTablesAsync(tables, cancellationToken).ConfigureAwait(false);
                return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            using (var transaction = Database.BeginTransaction())
            {
                await PrepareIdentityTablesAsync(tables, cancellationToken).ConfigureAwait(false);
                int affectedRows = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                transaction.Commit();
                return affectedRows;
            }
        }

        private string[] GetAddedIdentityTables()
        {
            return ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Added)
                .Select(entry => ObjectContext.GetObjectType(entry.Entity.GetType()))
                .Where(type => IdentityEntityTypes.Contains(type))
                .Select(type => type.Name)
                .Distinct()
                .OrderBy(name => name, StringComparer.Ordinal)
                .ToArray();
        }

        private void PrepareIdentityTables(IEnumerable<string> tables)
        {
            foreach (string table in tables)
                Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    BuildIdentityPreparationSql(table));
        }

        private async Task PrepareIdentityTablesAsync(
            IEnumerable<string> tables, CancellationToken cancellationToken)
        {
            foreach (string table in tables)
                await Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction,
                    BuildIdentityPreparationSql(table), cancellationToken).ConfigureAwait(false);
        }

        private static string BuildIdentityPreparationSql(string table)
        {
            // Names come exclusively from IdentityEntityTypes, never user input.
            // Keep the exclusive lock until the INSERT and its transaction finish.
            return $@"
DECLARE @maximumId BIGINT;
SELECT @maximumId = COALESCE(MAX([{table}Id]), 0)
FROM [dbo].[{table}] WITH (TABLOCKX, HOLDLOCK);

DECLARE @lastIdentity BIGINT;
SELECT @lastIdentity = CONVERT(BIGINT, last_value)
FROM sys.identity_columns
WHERE object_id = OBJECT_ID(N'dbo.{table}');

-- NULL means a new/TRUNCATEd table: leave its original IDENTITY(1,1) seed alone.
-- After DELETE or a failed INSERT, reseeding to MAX (or 0) makes the next ID MAX+1.
IF @lastIdentity IS NOT NULL AND @lastIdentity <> @maximumId
    DBCC CHECKIDENT (N'dbo.{table}', RESEED, @maximumId) WITH NO_INFOMSGS;";
        }
    }
}
