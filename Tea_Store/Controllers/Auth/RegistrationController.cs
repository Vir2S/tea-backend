using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tea_Store.DTOs.UsersDTO;
using Tea_Store.Models;

namespace Tea_Store.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController(UserManager<User> userManager, IMapper mapper) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.Role = Role.user;
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully!" });
            }

            return BadRequest(result.Errors);
        }
    }
}
