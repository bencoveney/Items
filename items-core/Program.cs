using System;
using Microsoft.Data.Sqlite;

namespace items
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection("Data Source=.\\database\\chinook.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM Customers;";

                var result = command.ExecuteReader();

                Console.WriteLine(result.HasRows);

                while(result.Read())
                {
                    Console.WriteLine(result.GetString(0) + ", " + result.GetString(1) + ", " + result.GetString(2));
                }

                connection.Close();
            }
        }
    }
}
