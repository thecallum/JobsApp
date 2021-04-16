using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class SqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess()
        {
            _connectionString = GetConnectionString();
        }

        public List<T> LoadData<T, TU>(string sqlStatement, TU parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = connection.Query<T>(sqlStatement, parameters).ToList();

                return rows;
            }
        }

        public int SaveData<T>(string sqlStatement, T parameters, bool returnId = false)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = connection.Query<int>(sqlStatement, parameters);

                if (!returnId) return -1;

                var id = result.Single();

                return id;
            }
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            return config.GetConnectionString(connectionStringName);
        }
    }
}