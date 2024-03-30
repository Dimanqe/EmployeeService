using System;
using System.Threading.Tasks;
using EmployeeService.Contracts.Models.Department;
using EmployeeService.Data.Models;
using EmployeeService.Data.Repos.DepartmentRepo;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentManagerController : Controller
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentManagerController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] AddDepartmentRequest addDepartmentRequestRequest)
    {
        try
        {
            var department = new Department
            {
                Name = addDepartmentRequestRequest.Name,
                Phone = addDepartmentRequestRequest.Phone
            };

            await _departmentRepository.Create(department);
            Console.WriteLine($"Department {department.Name} created successfully.");
            return Ok($"Department {department.Name} created successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the department.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentRepository.Delete(id);
            Console.WriteLine($"Department with Id {id} deleted successfully.");
            return Ok($"Department with Id {id} deleted successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, $"An error occurred while deleting the department with Id {id}.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var department = await _departmentRepository.Get(id);
            if (department != null)
                return Ok(department);
            return NotFound($"Department with Id {id} not found.");
        }
        catch (Exception)
        {
            return StatusCode(500, $"An error occurred while retrieving the department with Id {id}.");
        }
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var departments = await _departmentRepository.GetAll();
            return Ok(departments);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving all departments.");
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateDepartmentRequest updateDepartmentRequest, int id)
    {
        try
        {
            var department = new Department
            {
                Name = updateDepartmentRequest.Name,
                Phone = updateDepartmentRequest.Phone
            };

            await _departmentRepository.Update(department, id);

            Console.WriteLine($"Department with Id {id} updated successfully.");

            return Ok($"Department with Id {id} updated successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the department.");
        }
    }
}