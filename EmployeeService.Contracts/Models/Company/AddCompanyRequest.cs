using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Models.Company
{
    public class AddCompanyRequest
    {
        [Required(ErrorMessage ="The Name field is required")]
        public string Name { get; set;}

        [Required(ErrorMessage = "The Address field is required")]
        public string Address { get; set;}

        [Required(ErrorMessage = "The Phone field is required")]
        public string Phone { get; set;}

    }
}
