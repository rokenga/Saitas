using System;
namespace ToursimApp
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleID { get; set; } = 1; // Default role

        public string Role
        {
            get
            {
                return GetRoleName(RoleID);
            }
        }

        public User(int iD, string email, byte[] passwordHash, byte[] passwordSalt, int roleID)
        {
            ID = iD;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            RoleID = roleID;
        }

        public User()
        {
        }

        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "User";
                case 2:
                    return "Admin";
                default:
                    return "Unknown"; 
            }
        }
    }
}

