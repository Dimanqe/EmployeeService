using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Models.Passport
{
    public class UpdatePassportRequest
    {
        public string Type { get; set; }
        
        public string Number { get; set; }
        
        
    }
}
