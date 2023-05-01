using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTasks>>> GetTaskDetails()
        {
          if (_context.TaskDetails == null)
          {
              return NotFound();
          }
            return await _context.TaskDetails.ToListAsync();
        }

        // GET: api/EmployeeTask/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTasks>> GetEmployeeTasks(Guid id)
        {
          if (_context.TaskDetails == null)
          {
              return NotFound();
          }
            var employeeTasks = await _context.TaskDetails.FindAsync(id);

            if (employeeTasks == null)
            {
                return NotFound();
            }

            return employeeTasks;
        }

        // PUT: api/EmployeeTask/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTasks(Guid id, EmployeeTasks employeeTasks)
        {
            if (id != employeeTasks.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(employeeTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTasksExists(id))
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

        // POST: api/EmployeeTask
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeTasks>> PostEmployeeTasks(EmployeeTasks employeeTasks)
        {
          if (_context.TaskDetails == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TaskDetails'  is null.");
          }
            _context.TaskDetails.Add(employeeTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeTasks", new { id = employeeTasks.TaskId }, employeeTasks);
        }

        // DELETE: api/EmployeeTask/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTasks(Guid id)
        {
            if (_context.TaskDetails == null)
            {
                return NotFound();
            }
            var employeeTasks = await _context.TaskDetails.FindAsync(id);
            if (employeeTasks == null)
            {
                return NotFound();
            }

            _context.TaskDetails.Remove(employeeTasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeTasksExists(Guid id)
        {
            return (_context.TaskDetails?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
