using System;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using NuGet.Protocol.Plugins;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace ToursimApp
{
    public static class CustomerData
    {
        // ar man daryti pagal single responsibility principle ar visus 5 SOLID principus
        // kai turiu ta customers data klase, 
        public static List<Customer> ReadCustomers()
        {
            string query = "SELECT * FROM customer";
            return QueryExecution.ExecuteQuery(query, reader => new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDateTime(3),
                reader.GetString(4),
                reader.GetString(5)));
        }

        public static void InsertCustomer(Customer customer)
        {
            string query = "INSERT INTO saitas.customer(customer_id, customer_name, customer_surname, customer_birthdate, customer_address, customer_email) VALUES (@id, @name, @surname, @birthdate, @address, @email)";
            QueryExecution.ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", customer.ID);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@surname", customer.Surname);
                cmd.Parameters.AddWithValue("@birthdate", customer.BirthDate);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@email", customer.Email);
            });
        }

        public static void RemoveCustomer(int id)
        {
            string query = "DELETE FROM saitas.customer WHERE customer_id = @id";
            QueryExecution.ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", id);
            });
        }

        public static Customer getCustomerById(int customerId)
        {
            string cs = @"server=localhost;userid=augis;password=;database=saitas";
            using var con = new MySqlConnection(cs);
            con.Open();
            var stm = "SELECT * FROM saitas.customer WHERE customer_id = @customerId";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Prepare();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                return null; 
            }
            rdr.Read();
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            string surname = rdr.GetString(2);
            DateTime birthdate = rdr.GetDateTime(3);
            string address = rdr.GetString(4);
            string email = rdr.GetString(5);
            return new Customer(id, name, surname, birthdate, address, email);
        }

        public static Customer GetCustomerByID(int id)
        {
            var query = "SELECT * FROM saitas.customer WHERE customer_id = @customerId";
            return QueryExecution.ExecuteSingleResult(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@customerId", id);
            }, reader =>
            {
                if (reader.HasRows)
                {
                    return new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetString(4),
                        reader.GetString(5));
                }
                return null;
            });
        }

    }
}


