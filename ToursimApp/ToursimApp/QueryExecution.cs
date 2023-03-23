using System;
using MySql.Data.MySqlClient;

namespace ToursimApp
{
	public class QueryExecution
	{
        // Executes a non-query SQL statement(insert or update)
        public static void ExecuteNonQuery(string query, Action<MySqlCommand> prepare)
        {
            string connectionString = @"server=localhost;userid=augis;password=;database=saitas";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            prepare(command);
            command.ExecuteNonQuery();
        }

        // Executes a query SQL statement(select) lalalal i dont get
        public static List<T> ExecuteQuery<T>(string query, Func<MySqlDataReader, T> map)
        {
            string connectionString = @"server=localhost;userid=augis;password=;database=saitas";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add(map(reader));
            }
            return result;
        }


        public static T ExecuteSingleResult<T>(string query, Action<MySqlCommand> prepare, Func<MySqlDataReader, T> map)
        {
            string connectionString = @"server=localhost;userid=augis;password=;database=saitas";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            using var command = new MySqlCommand(query, connection);
            prepare(command);
            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return map(reader);
            }
            return default(T);
        }
    }
}

