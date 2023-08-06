using System;
namespace ToursimApp
{
	public interface ICustomerData
	{
        void InsertCustomer(Customer customer);
        List<Customer> ReadCustomers();
        void RemoveCustomer(int id);
        Customer GetCustomerByID(int id);
    }
}

