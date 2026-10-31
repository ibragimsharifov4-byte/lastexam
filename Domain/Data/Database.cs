using Npgsql;

namespace Domain.Data;

public static class Database
{
    private static string connectionString = 
    @"Host = localhost;
    Port = 5432;
    Username = postgres;
    Database = shopy_db;
    Password = 1234";

    public static NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
