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
using Microsoft.Extensions.Configuration; 


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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user == null || !user.EmailConfirmed)
            {
                return Unauthorized(new { message = "Невірний email або користувач не підтвердив свою електронну пошту." });
            }
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Невірний пароль." });
            }



            var tokenString = GenerateJwtToken(user);

            return Ok(new { token = tokenString, message = "Ви успішно увійшли в систему!" });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
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
