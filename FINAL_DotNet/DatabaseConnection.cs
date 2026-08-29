using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace FINAL_DotNet
{
    public partial class QL_CuaHangDaQuy_PNJEntities
    {
        public QL_CuaHangDaQuy_PNJEntities(string connectionString)
            : base(connectionString)
        {
        }
    }

    internal static class DatabaseConnection
    {
        private const string Metadata =
            "res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";

        public static QL_CuaHangDaQuy_PNJEntities CreateContext()
        {
            string entityConnectionString = BuildEntityConnectionString();
            return entityConnectionString == null
                ? new QL_CuaHangDaQuy_PNJEntities()
                : new QL_CuaHangDaQuy_PNJEntities(entityConnectionString);
        }

        public static SqlConnection CreateSqlConnection(string initialCatalog = null)
        {
            string entityConnectionString = BuildEntityConnectionString();
            if (entityConnectionString == null)
            {
                ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["QL_CuaHangDaQuy_PNJEntities"];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                    throw new InvalidOperationException("Không tìm thấy cấu hình kết nối QL_CuaHangDaQuy_PNJEntities.");
                entityConnectionString = setting.ConnectionString;
            }

            var entityBuilder = new EntityConnectionStringBuilder(entityConnectionString);
            var sqlBuilder = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString);
            if (!string.IsNullOrWhiteSpace(initialCatalog)) sqlBuilder.InitialCatalog = initialCatalog.Trim();
            sqlBuilder.ApplicationName = "FINAL_DotNet Backup Restore";
            return new SqlConnection(sqlBuilder.ConnectionString);
        }

        public static string GetDatabaseName()
        {
            using (SqlConnection connection = CreateSqlConnection())
                return new SqlConnectionStringBuilder(connection.ConnectionString).InitialCatalog;
        }

        private static string BuildEntityConnectionString()
        {
            string server = GetEnvironmentValue("PNJ_DB_SERVER");
            if (server == null)
            {
                // Không cấu hình biến môi trường: dùng kết nối localhost trong App.config.
                return null;
            }

            string database = GetEnvironmentValue("PNJ_DB_NAME") ?? "QL_CuaHangDaQuy_PNJ";
            string username = GetEnvironmentValue("PNJ_DB_USER");
            string password = Environment.GetEnvironmentVariable("PNJ_DB_PASSWORD");

            if ((username == null) != (password == null))
            {
                throw new InvalidOperationException(
                    "Cần cấu hình đồng thời PNJ_DB_USER và PNJ_DB_PASSWORD.");
            }

            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                ConnectTimeout = 15,
                ApplicationName = "FINAL_DotNet"
            };

            if (username == null)
            {
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilder.UserID = username;
                sqlBuilder.Password = password;
            }

            var entityBuilder = new EntityConnectionStringBuilder
            {
                Metadata = Metadata,
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlBuilder.ConnectionString
            };

            return entityBuilder.ConnectionString;
        }

        private static string GetEnvironmentValue(string name)
        {
            string value = Environment.GetEnvironmentVariable(name);
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
