using System;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using NuGet.Protocol.Plugins;
using Dapper;
using MySqlX.XDevAPI.Common;

namespace ToursimApp
{
    public class CustomerData :ICustomerData
    {
        private readonly IQueryExecution queryExecution;

        public CustomerData(IQueryExecution queryExecution)
        {
            this.queryExecution = queryExecution;
        }

        public List<Customer> ReadCustomers()
        {
            string query = "SELECT customer_id as ID, customer_name as Name, customer_surname as Surname, customer_birthdate as Birthdate, customer_address as Address, customer_email as Email FROM customer";
            return queryExecution.DatabaseQuery<Customer>(query);
        }

        public void InsertCustomer(Customer customer)
        {
            var validator = new CustomerValidator();
            string validationError = validator.Validate(customer);

            if (validationError != null)
            {
                throw new Exception(validationError);
            }

            string query = "INSERT INTO saitas.customer(customer_id, customer_name, customer_surname, customer_birthdate, customer_address, customer_email) VALUES (@id, @name, @surname, @birthdate, @address, @email)";
            queryExecution.DatabaseExecute(query, new { id = customer.ID, name = customer.Name, surname = customer.Surname, birthdate = customer.BirthDate, address = customer.Address, email = customer.Email });
        }

        public void RemoveCustomer(int id)
        {
            string query = "DELETE FROM saitas.customer WHERE customer_id = @ID";
            queryExecution.DatabaseExecute(query, new { ID = id });
        }

        public Customer GetCustomerByID(int id)
        {
            var query = "SELECT customer_id as ID, customer_name as Name, customer_surname as Surname, customer_birthdate as BirthDate, customer_address as Address, customer_email as Email FROM saitas.customer WHERE customer_id = @customerId";
            var result = queryExecution.DatabaseQueryFirst(query, new { customerId = id });

            if (result != null)
            {
                return MapToCustomer(result);
            }

            return null;
        }

        // mapping to convert an object to a customer
        private Customer MapToCustomer(object data)
        {
            var customerData = (dynamic)data;
            var customer = new Customer
            {
                ID = customerData.ID,
                Name = customerData.Name,
                Surname = customerData.Surname,
                BirthDate = customerData.BirthDate,
                Address = customerData.Address,
                Email = customerData.Email
            };

            return customer;
        }


    }
}


