﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models.EntityModels
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public double Salary { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
