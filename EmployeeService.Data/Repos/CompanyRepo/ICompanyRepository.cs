using EmployeeService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
