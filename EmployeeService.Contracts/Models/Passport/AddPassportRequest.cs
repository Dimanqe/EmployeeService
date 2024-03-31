using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
