using System;
using System.Text.RegularExpressions;

namespace ToursimApp
{
    public class CustomerValidator
    {
        public string Validate(Customer customer)
        {
            if (customer.ID <= 0)
            {
                return "Please enter a valid ID";
            }

            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                return "Please enter the name";
            }

            if (!IsAlphabeticOnly(customer.Name))
            {
                return "The name must contain only letters";
            }

            if (string.IsNullOrWhiteSpace(customer.Surname))
            {
                return "Please enter the surname";
            }

            if (!IsAlphabeticOnly(customer.Surname))
            {
                return "The surname must contain only letters";
            }

            if (customer.BirthDate == default)
            {
                return "Please enter the birthdate";
            }

            if (!IsValidBirthdate(customer.BirthDate))
            {
                return "Please enter a valid birthdate";
            }

            if (!IsWithinValidBirthdate(customer.BirthDate))
            {
                return "Please enter a birthdate within a valid range";
            }

            if (!IsCorrectDateFormat(customer.BirthDate))
            {
                return "Please enter the birthdate in the format: yyyy-MM-dd";
            }

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                return "Please enter the home address";
            }

            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                return "Please enter the email";
            }

            if (!IsValidEmail(customer.Email))
            {
                return "Please enter a valid email address";
            }

            return null; 
        }

        private bool IsAlphabeticOnly(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        //private bool IsAlphabeticOnly(string value)
        //{
        //    return value.All(c => char.IsLetter(c));
        //}


        private bool IsValidBirthdate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool IsWithinValidBirthdate(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            return age > 0 && age <= 100;
        }

        private bool IsCorrectDateFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd");
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, pattern);
        }

    }
}


