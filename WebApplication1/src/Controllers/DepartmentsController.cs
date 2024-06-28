using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();

            var departmentDtos = departments.Select(d => new DepartmentDto
            {
                Id = d.DepID,
                Name = d.DepName,
                Location = d.DepLocation
            }).ToList();

            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound(new { Message = "Department not found" });
            }

            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department
            {
                DepName = createDepartmentDto.Name,
                DepLocation = createDepartmentDto.Location
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepID }, department);
        }
    }
}