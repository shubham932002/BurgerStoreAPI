using BurgerStoreAPI.BusinessLayer;
using BurgerStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BurgerStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        // DTO defined within the controller
        public class UserLoginDto
        {
            public string Name { get; set; }
            public string MobileNumber { get; set; }
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            // Validate user credentials
            var users = await _userService.GetAllUsersAsync();
            var validUser = users.FirstOrDefault(u =>
                u.MobileNumber == loginDto.MobileNumber && u.Name == loginDto.Name);

            if (validUser == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate JWT token
            var token = _tokenService.GenerateToken(validUser);

            return Ok(new { Token = token });
        }
    }
}

