using EmployeeService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
