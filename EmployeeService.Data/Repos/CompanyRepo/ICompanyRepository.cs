using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.CompanyRepo
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company> Get(int id);
        Task Create(Company company);
        Task Update(Company company, int id);
        Task Delete(int id);
    }
}