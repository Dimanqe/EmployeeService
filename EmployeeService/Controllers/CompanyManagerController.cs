using System;
using System.Threading.Tasks;
using EmployeeService.Contracts.Models.Company;
using EmployeeService.Data.Models;
using EmployeeService.Data.Repos.CompanyRepo;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyManagerController : Controller
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyManagerController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] AddCompanyRequest addCompanyRequest)
    {
        try
        {
            var company = new Company
            {
                Name = addCompanyRequest.Name,
                Address = addCompanyRequest.Address,
                Phone = addCompanyRequest.Phone
            };

            await _companyRepository.Create(company);
            Console.WriteLine($"Company {company.Name} created successfully.");
            return Ok($"Company {company.Name} created successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the company.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _companyRepository.Delete(id);
            Console.WriteLine($"Company with Id {id} deleted successfully.");
            return Ok($"Company with Id {id} deleted successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, $"An error occurred while deleting the company with Id {id}.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var company = await _companyRepository.Get(id);
            if (company != null)
                return Ok(company);
            return NotFound($"Company with Id {id} not found.");
        }
        catch (Exception)
        {
            return StatusCode(500, $"An error occurred while retrieving the company with Id {id}.");
        }
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var companies = await _companyRepository.GetAll();
            return Ok(companies);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while retrieving all companies.");
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateCompanyRequest updateCompanyRequestRequest, int id)
    {
        try
        {
            var company = new Company
            {
                Name = updateCompanyRequestRequest.Name,
                Address = updateCompanyRequestRequest.Address,
                Phone = updateCompanyRequestRequest.Phone
            };

            await _companyRepository.Update(company, id);

            Console.WriteLine($"Company with Id {id} updated successfully.");

            return Ok($"Company with Id {id} updated successfully.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the company.");
        }
    }
}