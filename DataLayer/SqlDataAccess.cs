using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<T>> LoadData<T, TU>(string sqlStatement, TU parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlStatement, parameters);

                return rows.ToList();
            }
        }

        public async Task<int> SaveData<T>(string sqlStatement, T parameters, bool returnId = false)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<int>(sqlStatement, parameters);

                if (!returnId) return -1;

                var id = result.Single();

                return id;
            }
        }

        public async Task UpdateData<T>(string sqlStatement, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlStatement, parameters);
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