﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSite.Models.Models.Employess;
using RazorPagesSite.Services.Employees;
using RazorPagesSite.Models.Models;
using Microsoft.AspNetCore.Http;

namespace RazorPagesUI.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Employee Employee { get; private set; }

        [BindProperty]
        public IFormFile Photo { get; private set; }

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
            if (Employee == null) return RedirectToPage("/NotFound");
            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {
            Employee = _employeeRepository.Update(employee);
            return RedirectToPage("Employees");
        }
    }
}