using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tea_Store.Models;
using System.IdentityModel.Tokens.Jwt;
using ViewModels.AuthController;

namespace Tea_Store.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TeaStoreSecretKey"));
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
