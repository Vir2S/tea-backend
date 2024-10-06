using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tea_Store.Models;
using ViewModels.AuthController;

namespace Tea_Store.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController(UserManager<User> userManager, IMapper mapper) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel registerViewModel)
        {
            var user = _mapper.Map<User>(registerViewModel);
            user.Role = Role.user;
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully!" });
            }

            return BadRequest(result.Errors);
        }
    }
}
