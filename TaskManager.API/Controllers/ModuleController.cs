using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.API.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModuleController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: api/<ModuleController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ModulesDTO>>> GetModuleDetails()
        {
            if (_context.ModuleDetails == null)
            {
                return NotFound();
            }
            var modules = await _context.ModuleDetails.ToListAsync();

            return Ok(modules.Select(u => new ModulesDTO
            {
                ModuleId = u.ModuleId,
                Tasks = u.Tasks,
                ModuleDescription = u.ModuleDescription,
                ModuleDateTime = u.ModuleDateTime,
                ModuleTotalTime = u.ModuleTotalTime,
                ModuleStatus = u.ModuleStatus
            }));
        }

        // GET api/<ModuleController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Modules>> GetModules(Guid id)
        {
            if (_context.ModuleDetails == null)
            {
                return NotFound();
            }
            var modules = await _context.ModuleDetails.FindAsync(id);

            if (modules == null)
            {
                return NotFound();
            }

            return modules;
        }

        // POST api/<ModuleController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Modules>> PostUserTasks(Modules modules)
        {
            if (_context.ModuleDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ModuleDetails'  is null.");
            }
            _context.ModuleDetails.Add(modules);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModules", new { id = modules.ModuleId }, modules);
        }

        // PUT api/<ModuleController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutModules(Guid id, Modules modules)
        {
            if (id != modules.ModuleId)
            {
                return BadRequest();
            }

            _context.Entry(modules).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModulesExists(id))
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
        private bool ModulesExists(Guid id)
        {
            return (_context.ModuleDetails?.Any(e => e.ModuleId == id)).GetValueOrDefault();
        }

        // DELETE api/<ModuleController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteModules(Guid id)
        {
            if (_context.ModuleDetails == null)
            {
                return NotFound();
            }
            var modules = await _context.ModuleDetails.FindAsync(id);
            if (modules == null)
            {
                return NotFound();
            }

            _context.ModuleDetails.Remove(modules);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
