using elfBeautyBrewery.Api.Application.Contracts.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace elfBeautyBrewery.Api.Host.Controllers
{
    [ApiController]
    [Route("v{apiVersion:apiVersion}/api/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Authenticates the user and returns a token if the credentials are valid.
        /// </summary>
        /// <param name="login">Login credentials including username and password.</param>
        /// <returns>Token or invalid credentials.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel login)
        {
            if (IsValidUser(login))
            {
                var token = GenerateToken(login.UserName);
                return Ok(new { token });
            }
            return Unauthorized("Invalid credentials");
        }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        private bool IsValidUser(LoginRequestModel login)
        {
            // Replace with real user validation logic as needed.
            return login.UserName == "elfAdmin" && login.Password == "elf@Admin";
        }

        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        private string GenerateToken(string username)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
