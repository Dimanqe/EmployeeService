using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.PassportRepo
{
    public interface IPassportRepository
    {
        Task<List<Passport>> GetAll();
        Task Create(Passport passport);
        Task Update(Passport passport, int id);
        Task Delete(int id);
    }
}