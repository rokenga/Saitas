using System;
using MySql.Data.MySqlClient;

namespace ToursimApp
{
	public interface IQueryExecution
	{
        void DatabaseExecute(string query, object blah);
        List<T> DatabaseQuery<T>(string query);
        object DatabaseQueryFirst(string query, object parameters);
    }
}

