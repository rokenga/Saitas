using System;
namespace ToursimApp
{
	public interface IUserData
	{
        List<User> ReadUser();
        void InsertUser(User user);
        void RemoveUser(int id);
        User GetUserByEmail(string email);
    }
}

