using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AssetManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var adminUsername = _configuration["AdminCredentials:Username"];
            var adminPassword = _configuration["AdminCredentials:Password"];

            if (login.Username == adminUsername && login.Password == adminPassword)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var jwtSettings = _configuration.GetSection("JwtSettings");
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)); 
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Ok(new { IsAuthenticated = true, UserName = User.Identity.Name });
            }

            return Ok(new { IsAuthenticated = false, UserName = "" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}