﻿namespace EmployeeService.Contracts.Models.Company
{
    public class UpdateCompanyRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}