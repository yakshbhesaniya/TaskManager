using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(ApplicationDbContext context, UserManager<Employee> userManager,
            SignInManager<Employee> signInManager, IConfiguration configuration)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Employee input)
        {
            if (input == null) { return BadRequest(); }
            var employee = await userManager.FindByNameAsync(input.EmployeeUsername);
            if (employee == null) { return NotFound(); }

            var res = await signInManager.CheckPasswordSignInAsync(employee, input.EmployeePassword, false);

            if (res.Succeeded)
            {
                var token = await GetAuthTokenAsync(employee);

                return Ok(new { token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        async Task<string> GetAuthTokenAsync(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Authentication").GetSection("JwtBearer").GetSection("SecurityKey").ToString());
            var MySymmetricSecurityKey = new SymmetricSecurityKey(key);
            var roles = await userManager.GetRolesAsync(employee);

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
                    new Claim(ClaimTypes.Name, employee.EmployeeUsername),
                    //new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId),
                }),
            };
            tokenDescriptor.Subject.AddClaims(claims);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
