using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.API.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(ApplicationDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO input)
        {
            if (input == null) { return BadRequest(); }
            var user = await userManager.FindByNameAsync(input.Email);
            if (user == null) { return NotFound(); }

            var res = await signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (res.Succeeded)
            {
                var token = await GetAuthTokenAsync(user);

                return Ok(new { token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        async Task<string> GetAuthTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Authentication").GetSection("JwtBearer").GetSection("SecurityKey").ToString());
            var MySymmetricSecurityKey = new SymmetricSecurityKey(key);
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = configuration.GetSection("Authentication").GetSection("JwtBearer").GetSection("Issuer").ToString(),
                Audience = configuration.GetSection("Authentication").GetSection("JwtBearer").GetSection("Audience").ToString(),
                SigningCredentials = new SigningCredentials(MySymmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
            };
            tokenDescriptor.Subject.AddClaims(claims);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(RegisterDTO input)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserDetails'  is null.");
            }

            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                PhoneNumber = input.Phone,
            };

            var result = await userManager.CreateAsync(user, input.Password);
            return Ok(result);

        }

    }
}
