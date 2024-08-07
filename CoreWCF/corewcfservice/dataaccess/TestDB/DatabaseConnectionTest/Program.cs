using System;
using System.Data.SqlClient;
 //using Microsoft.Data.SqlClient;

namespace DatabaseConnectionTest{
public class Program
{
    public static void Main(string[] args)
    {
        var tester = new DatabaseConnectionTester();
        bool success = tester.TestConnection();
        Console.WriteLine("Connection test " + (success ? "succeeded." : "failed."));
    }
}

public class DatabaseConnectionTester
{
    private readonly string _connectionString = "Server=SURYA\\SQLEXPRESS;Database=VuejsApp;Integrated Security=True";

    public bool TestConnection()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection successful!");
                return true;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("SQL Exception: " + ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return false;
        }
    }
}
}