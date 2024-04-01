using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Contracts.Models.Department
{
    public class AddDepartmentRequest
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        public string Phone { get; set; }
    }
}