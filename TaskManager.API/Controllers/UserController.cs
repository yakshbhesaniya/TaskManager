using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
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
    public class UserController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
       

        public UserController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserDetails()
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.ToListAsync();

            return Ok(users.Select(u => new UserDTO
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role= u.Role,
                PhoneNumber = u.PhoneNumber,
            }));
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);

            if(users == null)
            {
                return NotFound();
            }

            return Ok( new UserDTO
            {
                UserId = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Role = users.Role,
                PhoneNumber = users.PhoneNumber,
            });
        }

        
        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserDTO input)
        {
            if(id != input.UserId)
            {
                return BadRequest();
            }

            _context.Entry(input).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!UserExists(id))
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

       

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            var employee = await _context.Users.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            _context.Users.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.UserDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
