using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<List<Employee>> GetByCompanyId(int id);
        Task<Employee> GetById(int id);
        Task<int> Create(Employee employee, Passport passport);
        Task Update(Employee employee, Passport passport, int id);
        Task Delete(int id);
        Task<int> GetId(Employee employee);
    }
}