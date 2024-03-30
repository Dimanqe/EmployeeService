using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Contracts.Models.Employee
{
    public class AddEmployeeRequest
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Surname field is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "The Phone field is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The CompanyId field is required.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "The DepartmentId field is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The PassportType field is required.")]
        public string PassportType { get; set; }

        [Required(ErrorMessage = "The PassportNumber field is required.")]
        public string PassportNumber { get; set; }
    }
}
