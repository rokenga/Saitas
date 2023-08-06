using System;
using System.Text.RegularExpressions;

namespace ToursimApp.Validators
{
	public class UserValidator
	{
        public string Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return "Please enter the email";
            }

            if (!IsValidEmail(user.Email))
            {
                return "Please enter a valid email address";
            }

            return null;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, pattern);
        }
    }
}

