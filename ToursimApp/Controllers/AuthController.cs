using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToursimApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserData UserData;
        private readonly IConfiguration _configuration;

        public AuthController(IUserData data, IConfiguration configuration)
        {
            UserData = data;
            _configuration = configuration;
        }

        //customers idet insert, isimt delete, perskaityti select where id
        //[HttpGet(Name = "GetUsers")]
        //public List<User> GetUsers()
        //{
        //    List<User> users = UserData.ReadUser();
        //    return users;
        //}

        //[HttpPost]
        //public IActionResult PostUser([FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    UserData.InsertUser(user);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    UserData.RemoveUser(id);
        //    return Ok();
        //}

        //[HttpGet("{email}")]
        //public User SelectUser(string email)
        //{
        //    User user1 = UserData.GetUserByEmail(email);

        //    return user1;
        //}

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var existingUser = UserData.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists. Please login or use a different email.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 1 
            };
            UserData.InsertUser(newUser);
            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var user = UserData.GetUserByEmail(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}

