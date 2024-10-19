using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tea_Store.Data;
using Tea_Store.Models;
using Tea_Store.Services;

using ViewModels.AuthController;

namespace Tea_Store.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController(TeaDBContext context, IMapper mapper, IPasswordHasher<User> passwordHasher, IEmailService emailService) : ControllerBase
    {
        private readonly TeaDBContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IEmailService _emailService = emailService;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerViewModel.Email || u.PhoneNumber == registerViewModel.PhoneNumber);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User with this email or phone number already exists." });
            }

            var user = _mapper.Map<User>(registerViewModel);
            user.Password = _passwordHasher.HashPassword(user, registerViewModel.Password);
            user.Role = Role.user;
            user.EmailConfirmed = false;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = Guid.NewGuid().ToString();
            user.EmailConfirmationToken = token;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Registration", new { token, email = user.Email }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email, "Email Confirmation", $"Please click the link to confirm your email: {confirmationLink}");

            return Ok(new { message = "A confirmation email has been sent to your email address!" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.EmailConfirmationToken == token);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid token or email address." });
            }

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var tokenString = GenerateJwtToken(user);

            return Ok(new { token = tokenString, message = "Email successfully confirmed!" });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("TeaStoreSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}