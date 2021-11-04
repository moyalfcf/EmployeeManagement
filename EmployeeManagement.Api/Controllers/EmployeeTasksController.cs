using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DataModel;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly EmployeeSystemContext _context;

        public EmployeeTasksController(EmployeeSystemContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTasks()
        {
            return await _context.EmployeeTasks.ToListAsync();
        }

        // GET: api/EmployeeTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTask>> GetEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);

            if (employeeTask == null)
            {
                return NotFound();
            }

            return employeeTask;
        }

        // GET: api/EmployeeTasks/ByEmployee/5
        [HttpGet("ByEmployee/{employeeID}")]
        public async Task<ActionResult<IEnumerable<EmployeeTask>>> GetEmployeeTasks(int employeeID)
        {
            var employee = await _context.Employees.FindAsync(employeeID);

            if (employee == null)
            {
                return NotFound();
            }

            await _context.Entry(employee).Collection(x => x.Tasks).LoadAsync();

            return new ActionResult<IEnumerable<EmployeeTask>>(employee.Tasks);
        }

        // PUT: api/EmployeeTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeTask(int id, EmployeeTask employeeTask)
        {
            if (id != employeeTask.ID)
            {
                return BadRequest();
            }

            _context.Entry(employeeTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTaskExists(id))
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

        // POST: api/EmployeeTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeTask>> PostEmployeeTask(EmployeeTask employeeTask)
        {
            _context.EmployeeTasks.Add(employeeTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeTask", new { id = employeeTask.ID }, employeeTask);
        }

        // DELETE: api/EmployeeTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            _context.EmployeeTasks.Remove(employeeTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeTaskExists(int id)
        {
            return _context.EmployeeTasks.Any(e => e.ID == id);
        }
    }
}
