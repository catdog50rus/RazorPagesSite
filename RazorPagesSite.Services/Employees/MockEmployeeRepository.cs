using RazorPagesSite.Models.Models.Employess;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesSite.Services.Employees
{
    /// <summary>
    /// Реализация временного репозитория
    /// </summary>
    public class MockEmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Список сотрудников
        /// </summary>
        private readonly List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            //Тестовая БД
            _employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 0, Name = "Mary", Email="Mary@example.com", PhotoPath="avatar_Mary.png", Department = Dept.HR
                },
                new Employee()
                {
                    Id = 1, Name = "John", Email="John@example.com", PhotoPath="avatar_John.png", Department = Dept.IT
                },
                new Employee()
                {
                    Id = 2, Name = "Mike", Email="Mike@example.com", PhotoPath="avatar_Mike.png", Department = Dept.Payroll
                },
                new Employee()
                {
                    Id = 3, Name = "Max", Email="Max@example.com", PhotoPath="avatar_Max.png", Department = Dept.Payroll
                },
                new Employee()
                {
                    Id = 4, Name = "Larry", Email="Larry@example.com", PhotoPath="noimage.png", Department = Dept.None
                },
                new Employee()
                {
                    Id = 5, Name = "Jenny", Email="Jenny@example.com", PhotoPath="avatar_Jenny.png", Department = Dept.HR
                }
            };
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        /// <summary>
        /// Получить сотрудника по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Обновить данные сотрудника
        /// </summary>
        /// <param name="updateEmployee"></param>
        /// <returns></returns>
        public Employee Update(Employee updateEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == updateEmployee.Id);
            if (employee != null)
            {
                employee.Name = updateEmployee.Name;
                employee.Email = updateEmployee.Email;
                employee.Department = updateEmployee.Department;
                employee.PhotoPath = updateEmployee.PhotoPath;
                return employee;
            }
            else return updateEmployee;
        }
    }
}
