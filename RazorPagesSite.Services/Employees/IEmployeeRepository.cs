using System.Collections.Generic;
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
