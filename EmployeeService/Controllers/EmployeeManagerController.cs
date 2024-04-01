using System;
using System.Threading.Tasks;
using EmployeeService.Contracts.Models.Employee;
using EmployeeService.Data.Models;
using EmployeeService.Data.Repos.EmployeeRepo;
using EmployeeService.Data.Repos.PassportRepo;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeManagerController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPassportRepository _passportRepository;

    public EmployeeManagerController(IEmployeeRepository employeeRepository, IPassportRepository passportRepository)
    {
        _employeeRepository = employeeRepository;
        _passportRepository = passportRepository;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] AddEmployeeRequest addEmployeeRequest)
    {
        try
        {
            var employee = new Employee
            {
                Name = addEmployeeRequest.Name,
                Surname = addEmployeeRequest.Surname,
                Phone = addEmployeeRequest.Phone,
                CompanyId = addEmployeeRequest.CompanyId,
                DepartmentId = addEmployeeRequest.DepartmentId
            };

            var passport = new Passport
            {
                Type = addEmployeeRequest.PassportType,
                Number = addEmployeeRequest.PassportNumber
            };

            var passportId = await _employeeRepository.Create(employee, passport);

            var employeeId = await _employeeRepository.GetId(employee);
            Console.WriteLine($"Employee with id {employeeId} created successfully.");

            return Ok($"Employee with id {employeeId} created successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the employee.");
        }
    }


    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _employeeRepository.Delete(id);

            Console.WriteLine($"Employee with Id {id} deleted successfully.");

            return Ok($"Employee with Id {id} deleted successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while deleting the employee.");
        }
    }

    [HttpGet("GetAllEmployees")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var employees = await _employeeRepository.GetAll();

            Console.WriteLine("Retrieved all employees successfully.");

            return Ok(employees);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving all employees.");
        }
    }

    [HttpGet("GetAllByCompanyId/{id}")]
    public async Task<IActionResult> GetAllByCompanyId(int id)
    {
        try
        {
            var employees = await _employeeRepository.GetByCompanyId(id);

            Console.WriteLine("Retrieved all employees successfully.");

            return Ok(employees);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving all employees.");
        }
    }


    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeRequest updateEmployee, int id)
    {
        try
        {
            var employee = new Employee
            {
                Name = updateEmployee.Name,
                Surname = updateEmployee.Surname,
                Phone = updateEmployee.Phone,
                CompanyId = updateEmployee.CompanyId,
                DepartmentId = updateEmployee.DepartmentId
            };

            var passport = new Passport
            {
                Type = updateEmployee.PassportType,
                Number = updateEmployee.PassportNumber
            };

            await _employeeRepository.Update(employee, passport, id);

            Console.WriteLine($"Employee with Id {id} updated successfully.");

            return Ok($"Employee with Id {id} updated successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the employee.");
        }
    }
}