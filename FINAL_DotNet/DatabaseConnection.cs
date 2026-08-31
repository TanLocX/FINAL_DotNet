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

        public static QL_CuaHangDaQuy_PNJEntities CreateContext(string targetDatabase = null)
        {
            string entityConnectionString = BuildEntityConnectionString(targetDatabase);
            if (string.IsNullOrWhiteSpace(targetDatabase) && entityConnectionString == null)
            {
                return new QL_CuaHangDaQuy_PNJEntities();
            }
            return new QL_CuaHangDaQuy_PNJEntities(entityConnectionString);
        }

        public static string GetDatabaseName()
        {
            using (var db = CreateContext())
            {
                return db.Database.Connection.Database;
            }
        }

        private static string BuildEntityConnectionString(string targetDatabase = null)
        {
            string server = GetEnvironmentValue("PNJ_DB_SERVER");
            if (server == null)
            {
                if (string.IsNullOrWhiteSpace(targetDatabase))
                {
                    // Không cấu hình biến môi trường và không đổi DB: dùng kết nối localhost trong App.config.
                    return null;
                }

                ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["QL_CuaHangDaQuy_PNJEntities"];
                if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
                    throw new InvalidOperationException("Không tìm thấy cấu hình kết nối QL_CuaHangDaQuy_PNJEntities.");

                var entityBuilder = new EntityConnectionStringBuilder(setting.ConnectionString);
                var sqlBuilder = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString)
                {
                    InitialCatalog = targetDatabase.Trim()
                };
                entityBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;
                return entityBuilder.ConnectionString;
            }

            string database = !string.IsNullOrWhiteSpace(targetDatabase)
                ? targetDatabase.Trim()
                : (GetEnvironmentValue("PNJ_DB_NAME") ?? "QL_CuaHangDaQuy_PNJ");
            string username = GetEnvironmentValue("PNJ_DB_USER");
            string password = Environment.GetEnvironmentVariable("PNJ_DB_PASSWORD");

            if ((username == null) != (password == null))
            {
                throw new InvalidOperationException(
                    "Cần cấu hình đồng thời PNJ_DB_USER và PNJ_DB_PASSWORD.");
            }

            var sqlBuilderEnv = new SqlConnectionStringBuilder
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
                sqlBuilderEnv.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilderEnv.UserID = username;
                sqlBuilderEnv.Password = password;
            }

            var entityBuilderEnv = new EntityConnectionStringBuilder
            {
                Metadata = Metadata,
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlBuilderEnv.ConnectionString
            };

            return entityBuilderEnv.ConnectionString;
        }

        private static string GetEnvironmentValue(string name)
        {
            string value = Environment.GetEnvironmentVariable(name);
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
