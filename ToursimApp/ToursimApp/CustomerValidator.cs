using System;
using System.Globalization;
using FluentValidation;
namespace ToursimApp
{
	public class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			RuleFor(x => x.ID)
				.NotEmpty()
				.WithMessage("Please enter the ID");

			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Please enter the name")
				.Matches(@"^[A-Za-z]+$")
				.WithMessage("The name must contain only letters");

			RuleFor(x => x.Surname)
				.NotEmpty()
				.WithMessage("Please enter the surname")
				.Matches(@"^[A-Za-z]+$")
				.WithMessage("The surname must contain only letters");

			RuleFor(x => x.BirthDate)
				.NotEmpty()
				.WithMessage("Please enter the birthdate")
				.Must(ValidBirthdate)
				.WithMessage("Please enter a valid birthdate")
				.Must(BeWithinValidBirthdate)
				.WithMessage("Please enter a valid birthdate")
				.Must(BeInCorrectFormat)
				.WithMessage("Please enter in format: yyyy-MM-dd");

			RuleFor(x => x.Address)
				.NotEmpty()
				.WithMessage("Please enter the home address");

			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Please enter the email")
				.EmailAddress()
				.WithMessage("Please enter a valid email address");
        }

		private bool ValidBirthdate(DateTime date)
		{
			return !date.Equals(default(DateTime));
		}

		private bool BeWithinValidBirthdate(DateTime date)
		{
			int age = DateTime.Today.Year - date.Year;
			return age >= 0 && age <= 100;
		}

		private bool BeInCorrectFormat(DateTime date)
		{
			return DateTime.TryParseExact(date.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
		}

    }
}

