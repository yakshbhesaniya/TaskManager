using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class EmployeeController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDetails()
        {
            if(_context.EmployeeDetails == null)
            {
                return NotFound();
            }
            return await _context.EmployeeDetails.ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if(_context.EmployeeDetails == null)
            {
                return NotFound();
            }
            var employee = await _context.EmployeeDetails.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        /*[HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Employee input)
        {
            if(input == null) { return BadRequest(); }
            var employee = await userManager.FindByNameAsync(input.EmployeeUsername);
            if(employee == null) { return NotFound(); }

            var res = await signInManager.CheckPasswordSignInAsync(employee, input.EmployeePassword, false);

            if(res.Succeeded)
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

        }*/

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if(id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if(_context.EmployeeDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeDetails'  is null.");
            }
            _context.EmployeeDetails.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if(_context.EmployeeDetails == null)
            {
                return NotFound();
            }
            var employee = await _context.EmployeeDetails.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            _context.EmployeeDetails.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.EmployeeDetails?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
