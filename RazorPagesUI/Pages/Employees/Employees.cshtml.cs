using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorPagesSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSite.Services.Employees;
using RazorPagesSite.Models.Models.Employess;

namespace RazorPagesUI.Pages.Employees
{
    public class EmployeesModel : PageModel
    {
        private readonly IEmployeeRepository _db;

        public IEnumerable<Employee> Employees { get; set; }

        public EmployeesModel(IEmployeeRepository db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Employees = _db.GetAllEmployees();
        }
    }
}