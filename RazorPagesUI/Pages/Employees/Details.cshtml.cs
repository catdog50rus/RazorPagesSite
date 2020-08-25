using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSite.Models.Models.Employess;
using RazorPagesSite.Services.Employees;

namespace RazorPagesUI.Pages.Employees
{
    public class DetailsModel : PageModel
    {

        public Employee Employee { get; private set; }

        private readonly IEmployeeRepository _employeeRepository;

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
            if (Employee == null) return RedirectToPage("/NotFound");
            return Page();
        }
    }
}