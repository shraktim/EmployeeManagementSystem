﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Models
{
    public class DepartmentEmployeeViewModel
    {
        public List<SelectListItem> Departments { get; set; }
    }
}
