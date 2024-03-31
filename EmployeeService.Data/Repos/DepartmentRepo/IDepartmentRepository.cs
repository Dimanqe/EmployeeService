using EmployeeService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
