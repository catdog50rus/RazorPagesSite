using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSite.Models.Models.Employess;
using RazorPagesSite.Services.Employees;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RazorPagesUI.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Employee Employee { get; private set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);
            if (Employee == null) return RedirectToPage("/NotFound");
            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {

            if (Photo != null)
            {
                if (employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }

                employee.PhotoPath = GetPathUploadedFile();
            }
            Employee = _employeeRepository.Update(employee);
            return RedirectToPage("Employees");
        }

        private string GetPathUploadedFile()
        {
            string result = null;
            if(Photo != null)
            {
                //Получаем конечную папку
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                //Получаем уникальное имя файла с фотографией
                result = $"{Guid.NewGuid()}{Photo.FileName}";
                //Получаем полный путь файла на сервере
                string filePath = Path.Combine(uploadsFolder, result);

                //Копируем файл с фотографией на сервер
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }


            }
            return result;
        }
    }
}