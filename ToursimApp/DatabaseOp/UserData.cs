using System;
using ToursimApp.Validators;

namespace ToursimApp.DatabaseOp
{
	public class UserData : IUserData
	{
        private readonly IQueryExecution queryExecution;

        public UserData(IQueryExecution queryExecution)
        {
            this.queryExecution = queryExecution;
        }

        public List<User> ReadUser()
        {
            string query = "SELECT id as ID, email as Email, password_hash as PasswordHash, password_salt as PasswordSalt, role as Role FROM user";
            return queryExecution.DatabaseQuery<User>(query);
        }

        public void InsertUser(User user)
        {
            var validator = new UserValidator();
            string validationError = validator.Validate(user);

            if (validationError != null)
            {
                throw new Exception(validationError);
            }

            string query = "INSERT INTO saitas.user(email, password_hash, password_salt, role) VALUES (@email, @password_hash, @password_salt, @role)";
            queryExecution.DatabaseExecute(query, new { email = user.Email, password_hash = user.PasswordHash, password_salt = user.PasswordSalt, role = user.RoleID });
        }

        public void RemoveUser(int id)
        {
            string query = "DELETE FROM saitas.user WHERE id = @ID";
            queryExecution.DatabaseExecute(query, new { ID = id });
        }

        public User GetUserByEmail(string email)
        {
            var query = "SELECT id as ID, email as Email, password_hash as PasswordHash, password_salt as PasswordSalt, role as Role FROM saitas.user WHERE email = @Email";

            var result = queryExecution.DatabaseQueryFirst(query, new { Email = email });

            if (result != null)
            {
                return MapToUser(result);
            }

            return null;
        }

        // mapping to convert an object to an user
        private User MapToUser(object data)
        {
            var userData = (dynamic)data;
            var user = new User
            {
                ID = userData.ID,
                Email = userData.Email,
                PasswordHash = userData.PasswordHash,
                PasswordSalt = userData.PasswordSalt,
                RoleID = userData.Role
            };

            return user;
        }
    }
}

