using Microsoft.Data.SqlClient;  // Use this for SQL Server
using System.Data;

namespace IIS___Project_2.Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection()
        {
            _connectionString = "Server=localhost;Database=marvelwebapp;Trusted_Connection=True;";
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
