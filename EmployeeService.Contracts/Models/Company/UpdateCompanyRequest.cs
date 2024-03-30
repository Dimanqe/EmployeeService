﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Models.Company
{
    public class UpdateCompanyRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        
    }
}
