using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLessons.Services;
using RazorPagesLessons.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RazorPagesGeneral.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        [BindProperty]
        public Employee Employee { get; set; }
        [BindProperty]
        public IFormFile Photo { get;  set; }
        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public IActionResult OnPost(Employee employee)
        {
            if (Photo != null)
            {
                if (Employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                Employee.PhotoPath = ProcessUploadedFile();
            }
            Employee = _employeeRepository.Update(employee);

            TempData["SuccessMessage"] = $"Update {Employee.Name} successful!";

            return RedirectToPage("Employees");
        }
        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications!";
            }
            else 
            {
                Message = "You have turned off email notifications!";
            }
            Employee = _employeeRepository.GetEmployee(id);
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath,FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }
            return uniqueFileName;
            
        }
    }
}
