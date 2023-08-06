using System;
using MySql.Data.MySqlClient;
using Dapper;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace ToursimApp
{
	public class QueryExecution : IQueryExecution
	{
        private readonly string connectionString;

        public QueryExecution(string connectionstring)
        {
            connectionString = connectionstring;
        }

        public void DatabaseExecute(string query, object parameters)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            connection.Execute(query, parameters);
        }

        public List<T> DatabaseQuery<T>(string query)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            return connection.Query<T>(query).ToList();
        }

        public object DatabaseQueryFirst(string query, object parameters)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var result = connection.QueryFirstOrDefault(query, parameters);

            if (result == null)
            {
                throw new ApplicationException("No data found for this ID");
            }

            return result;
        }
    }
}

