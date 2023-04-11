using System;

namespace ToursimApp
{
	public class Customer
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public string Address { get; set; }
        public string Email { get; set; }

        public Customer(int iD, string name, string surname, DateTime birthDate, string address, string email)
        {
            this.ID = iD;
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Address = address;
            this.Email = email;
        }
    }
}

