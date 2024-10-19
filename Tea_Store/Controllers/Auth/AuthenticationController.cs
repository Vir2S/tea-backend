using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tea_Store.Models;
using System.IdentityModel.Tokens.Jwt;
using ViewModels.AuthController;
using Tea_Store.Data;
using Microsoft.EntityFrameworkCore;


namespace Tea_Store.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(TeaDBContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration) : ControllerBase
    {
        private readonly TeaDBContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginViewModel.Email);

            if (user == null || !user.EmailConfirmed)
            {
                return Unauthorized(new { message = "Invalid email or the email has not been confirmed." });
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginViewModel.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Incorrect password." });
            }

            var tokenString = GenerateJwtToken(user);

            return Ok(new { token = tokenString, message = "Login successful!" });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TeaStoreIssue",
                audience: "TeaStoreAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
