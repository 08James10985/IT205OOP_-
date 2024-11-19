/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using resortM.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace resortM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new(); // Temporary in-memory list of users
        private readonly AuthHelper _authHelper;

        public UserController(IConfiguration configuration)
        {
            _authHelper = new AuthHelper(configuration);
        }

        // POST: api/User/Register
        [HttpPost("Register")]
        public IActionResult Register(UserDto request)
        {
            if (_users.Any(u => u.Username == request.Username ))
            {
                return BadRequest("Username or Email already exists.");
            }

            var (passwordHash, passwordSalt) = _authHelper.CreatePasswordHash(request.Password);

            var user = new User
            {
                Id = _users.Count + 1,
                Username = request.Username,
                
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _users.Add(user);
            return Ok("User registered successfully.");
        }

        // POST: api/User/Login
        [HttpPost("Login")]
        public IActionResult Login(UserDto request)
        {
            var user = _users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null || !_authHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _authHelper.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        // Example protected endpoint
        [HttpGet("Protected"), Authorize]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data accessible only with a valid token.");
        }
    }
}*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using resortM.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using resortM.Models.DTO;


namespace resortM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new();
        private readonly AuthHelper _authHelper;

        public UserController(IConfiguration configuration)
        {
            _authHelper = new AuthHelper(configuration);
        }

        [HttpPost("Register")]
        public IActionResult Register(LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_users.Any(u => u.Username == request.Username))
            {
                return BadRequest("Username already exists.");
            }

            var (passwordHash, passwordSalt) = _authHelper.CreatePasswordHash(request.Password);

            var user = new User
            {
                Id = _users.Count + 1,
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Platform = request.Platform
            };

            _users.Add(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("Login")]
        public IActionResult Login(UserDto request)
        {
            var user = _users.SingleOrDefault(u => u.Username == request.Username);

            if (user == null || !_authHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _authHelper.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpGet("Protected")]
        [Authorize]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data accessible only with a valid token.");
        }
    }
}

