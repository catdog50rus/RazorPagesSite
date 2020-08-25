using System;
using System.Collections.Generic;
using System.Text;
using RazorPagesSite.Models.Models;
using RazorPagesSite.Models.Models.Employess;

namespace RazorPagesSite.Services.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Update(Employee updateEmployee);
    }
}
