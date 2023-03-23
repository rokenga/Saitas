using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;

namespace ToursimApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaitasController : ControllerBase
    {
        //customers idet insert, isimt delete, perskaityti select where id
        [HttpGet(Name = "GetCustomers")]
        public List<Customer> GetCustomer()
        {
            List<Customer> customers = CustomerData.ReadCustomers();
            return customers;
        }

        [HttpPost]
        public IActionResult PostCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            CustomerData.InsertCustomer(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            CustomerData.RemoveCustomer(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public Customer SelectCustomer(int id)
        {
            Customer customer1 = CustomerData.GetCustomerByID(id);

            return customer1;
        }
    }
}
