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
using MySql.Data.MySqlClient;


namespace resortM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const string Salt = ")GN#447#^nryrETNwrbR%#&NBRE%#%BBDT#%";

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString => _configuration.GetConnectionString("DefaultConnection");
        [HttpPost("Register")]
        public ActionResult Register([FromBody] UserRegistrationDto newUser)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                // Check existing user
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", newUser.Username);
                    checkCommand.Parameters.AddWithValue("@Email", newUser.Email);

                    var count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                        return BadRequest("Username or Email already exists.");
                    }
                }

                // Hash password
                string hashedPassword = HashPassword(newUser.Password);

                // Determine role
                string role = newUser.AdminRegistrationKey == "YOUR_SECURE_ADMIN_REGISTRATION_KEY"
                    ? "admin"
                    : "user";

                // Insert user
                string insertQuery = @"
        INSERT INTO Users 
        (Username, Email, PasswordHash, PasswordSalt, FirstName, LastName, Platform, Role) 
        VALUES 
        (@Username, @Email, @PasswordHash, @PasswordSalt, @FirstName, @LastName, @Platform, @Role)";

                using (var insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Username", newUser.Username);
                    insertCommand.Parameters.AddWithValue("@Email", newUser.Email);
                    insertCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@PasswordSalt", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    insertCommand.Parameters.AddWithValue("@LastName", newUser.LastName);
                    insertCommand.Parameters.AddWithValue("@Platform", newUser.Platform);
                    insertCommand.Parameters.AddWithValue("@Role", role);

                    insertCommand.ExecuteNonQuery();
                }
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] UserLoginDto loginDto)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @Username";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", loginDto.Username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return Unauthorized("Invalid login credentials.");
                        }

                        string storedPassword = reader["PasswordHash"].ToString();
                        string inputPassword = HashPassword(loginDto.Password);

                        if (storedPassword != inputPassword)
                        {
                            return Unauthorized("Invalid login credentials.");
                        }

                        var user = new UserDto
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Role = reader["Role"].ToString()
                        };

                        var token = GenerateJwtToken(user, loginDto.Platform);
                        return Ok(new { Token = token, User = user });
                    }
                }
            }
        }

        [HttpGet("Profile")]
        [Authorize]
        public ActionResult GetUserProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT UserId, Username, Email, FirstName, LastName, Role FROM Users WHERE UserId = @UserId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return NotFound("User profile not found.");
                        }

                        var profile = new
                        {
                            UserId = reader["UserId"],
                            Username = reader["Username"],
                            Email = reader["Email"],
                            FirstName = reader["FirstName"],
                            LastName = reader["LastName"],
                            Role = reader["Role"]
                        };

                        return Ok(profile);
                    }
                }
            }
        }

        private string GenerateJwtToken(UserDto user, string platform)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("Username", user.Username),
            new Claim("Role", user.Role),
            new Claim("Platform", platform),

              new Claim(ClaimTypes.Role, user.Role) // Add this line
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Salt + password;
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

