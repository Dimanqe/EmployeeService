using EmployeeService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data.Repos.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<List<Employee>> GetByCompanyId(int id);
        Task<Employee> GetById(int id);
        Task Create(Employee employee);
        Task Update(Employee employee, int id);
        Task Delete(int id);
        Task<int> GetId(Employee employee);

    }
}
