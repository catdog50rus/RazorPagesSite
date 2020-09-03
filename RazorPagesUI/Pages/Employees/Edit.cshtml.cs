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

        [BindProperty] //Объявление свойства из формы
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }

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
                //Если у сотрудника была фотография
                //Удаляем ее
                if (employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                //Добавление новой фотографии
                //Копируем файл в БД
                //Получаем путь к новому файлу
                employee.PhotoPath = GetUploadedFilename();
            }
            //Обновляем профиль сотрудника в БД
            Employee = _employeeRepository.Update(employee);
            TempData["SeccessMessage"] = $"Пользователь {Employee.Name} успешно обновлен!";
            return RedirectToPage("Employees");
        }

        public void OnPostNotification(int id)
        {
            if (Notify)
                Message = "Оповещение подключено!";
            else
                Message = "оповещения отключено!";
            Employee = _employeeRepository.GetEmployee(id);

        }

        /// <summary>
        /// Скопировать новый файл в БД
        /// Вернуть имя нового файла
        /// </summary>
        /// <returns></returns>
        private string GetUploadedFilename()
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
                using var fs = new FileStream(filePath, FileMode.Create);
                Photo.CopyTo(fs);
            }
            return result;
        }
    }
}