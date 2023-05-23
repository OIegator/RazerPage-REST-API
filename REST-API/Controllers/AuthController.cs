using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // In-memory database representing login and password data
        private readonly List<User> users = new List<User>
        {
            new User { Username = "oleg", Password = "1234" },
        };

        private readonly string secretKey = "your_secret_key";
        private readonly string issuer = "your_issuer";
        private readonly string audience = "your_audience";
        private readonly int expirationMinutes = 60;

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Simulate authentication against the in-memory database
            var user = users.Find(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized(); // Invalid credentials
            }

            // Generate JWT token
            var token = GenerateJwtToken(request.Username);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();


            return tokenHandler.WriteToken(token);
        }

        public class AuthOptions
        {
            public const string ISSUER = "my_issuer";
            public const string AUDIENCE = "my_audience"; 
            const string KEY = "mine_secret_key_that_is_at_least_128_bits_long";   
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }

        // Request model for login
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // User model for in-memory database
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
