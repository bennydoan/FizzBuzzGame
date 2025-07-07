using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FizzbuzzAPI.Models.Auth;

namespace FizzbuzzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid input data"
                });
            }

            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Username already exists"
                });
            }

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email already exists"
                });
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "User registered successfully",
                    UserName = user.UserName
                });
            }

            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid input data"
                });
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid username or password"
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Token = token,
                    UserName = user.UserName
                });
            }

            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = "Invalid username or password"
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
