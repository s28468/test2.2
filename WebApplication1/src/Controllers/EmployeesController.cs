using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.DTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
        {
            var manager = await _context.Employees.FindAsync(new object[] { createEmployeeDto.ManagerId }, cancellationToken);
            var department = await _context.Departments.FindAsync(new object[] { createEmployeeDto.DepId }, cancellationToken);

            if (createEmployeeDto.ManagerId != null && manager == null)
            {
                return BadRequest(new { Message = "Manager not found" });
            }

            if (department == null)
            {
                return BadRequest(new { Message = "Department not found" });
            }

            var employee = new Employee
            {
                EmpName = createEmployeeDto.Name,
                JobName = createEmployeeDto.Job,
                ManagerID = createEmployeeDto.ManagerId,
                Salary = createEmployeeDto.Salary,
                Commission = createEmployeeDto.Comission,
                DepID = createEmployeeDto.DepId,
                HireDate = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(CreateEmployee), new { id = employee.EmpID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee, CancellationToken cancellationToken)
        {
            if (id != employee.EmpID)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound(new { Message = "Employee not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);
            if (employee == null)
            {
                return NotFound(new { Message = "Employee not found" });
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }
    }
}
