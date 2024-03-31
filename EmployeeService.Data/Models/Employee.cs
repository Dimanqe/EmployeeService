using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace EmployeeService.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int? PassportId { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public Passport Passport { get; set; }
        public Department Department { get; set; }
        public Company Company { get; set; }
    }

}
