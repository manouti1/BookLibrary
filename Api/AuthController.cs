using BookLibrary.Application;
using BookLibrary.Domain;
using BookLibrary.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserService _userService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthController(JwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        // POST /api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest("Invalid user data.");

            var existingUser = await _userService.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
                return Conflict("User already exists.");

            await _userService.RegisterUserAsync(user);
            return Ok("User registered successfully.");
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return BadRequest("Invalid login credentials.");

            var authenticatedUser = await _userService.AuthenticateUserAsync(user.Username, user.Password);
            if (authenticatedUser == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtTokenService.GenerateToken(user.Username);
            return Ok(new { Token = token });
        }
    }

}
