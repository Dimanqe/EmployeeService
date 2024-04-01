using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Contracts.Models.Company
{
    public class AddCompanyRequest
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Address field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        public string Phone { get; set; }
    }
}