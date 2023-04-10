using System;
using System.ComponentModel.DataAnnotations;

namespace ToursimApp
{
	public class Customer
	{
        [Required(ErrorMessage = "Please enter the ID")]
		public int ID { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name must contain only letters")]
		public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the surname")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Surname must contain only letters")]
		public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter the birthdate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MinimumAge(0, ErrorMessage = "Birthdate must be valid")]
        [MaximumAge(100, ErrorMessage = "Birthdate must be valid")]
		public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter the home address")]
		public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the email address")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Please enter a valid email address")]
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

    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            if(value is DateTime birthDate)
            {
                return (DateTime.Today.Year - birthDate.Year) >= _minimumAge;
            }
            return false;
        }
    }

    public class MaximumAgeAttribute : ValidationAttribute
    {
        private readonly int _maximumAge;

        public MaximumAgeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
        }

        public override bool IsValid(object value)
        {
            if(value is DateTime birthdate)
            {
                return (DateTime.Today.Year - birthdate.Year) <= _maximumAge;
            }
            return false;
        }
    }


}

