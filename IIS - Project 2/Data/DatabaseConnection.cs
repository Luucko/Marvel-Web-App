using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection()
    {
        _connectionString = "Server=localhost;Database=marvelwebapp;Trusted_Connection=True;";
    }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString); // Returns the connection to the database
    }
}
