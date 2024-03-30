using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Models.Employee
{
    public class UpdateEmployeeRequest
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Phone { get; set; }
        
        public int? CompanyId { get; set; }

        public int? DepartmentId { get; set; }
        
        public string PassportType { get; set; }
        
        public string PassportNumber { get; set; }
    }
}
