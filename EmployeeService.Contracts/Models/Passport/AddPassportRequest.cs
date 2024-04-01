using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Contracts.Models.Passport
{
    public class AddPassportRequest
    {
        [Required(ErrorMessage = "The Type field is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "The Number field is required.")]
        public string Number { get; set; }
    }
}