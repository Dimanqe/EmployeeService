using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAll();
        Task<Department> Get(int id);
        Task Create(Department department);
        Task Update(Department department, int id);
        Task Delete(int id);
    }
}