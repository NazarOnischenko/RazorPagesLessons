using RazorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorPagesLessons.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 0,
                    Name = "Nazar",
                    Email = "nazar9559@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "avatar.png"
                },
                new Employee()
                {
                    Id = 1,
                    Name = "Mary",
                    Email = "mary95@gmail.com",
                    Department = Dept.HR,
                    PhotoPath = "avatar2.png"
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Max",
                    Email = "max2662@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "avatar3.png"
                },
                new Employee()
                {
                    Id = 3,
                    Name = "Mykola",
                    Email = "mykola2222@gmail.com",
                    Department = Dept.IT,
                    PhotoPath = "avatar4.png"
                },
                new Employee()
                {
                    Id = 4,
                    Name = "Denis",
                    Email = "den3003@gmail.com",
                    Department = Dept.Payroll,
                    PhotoPath = "avatar5.png"
                },
                new Employee()
                {
                    Id = 5,
                    Name = "Nastya",
                    Email = "nastya4334@gmail.com",
                    Department = Dept.HR
                }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(item => item.Id == id);
        }

        public Employee Update(Employee updatedEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(item => item.Id == updatedEmployee.Id);

            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Department = updatedEmployee.Department;
                employee.PhotoPath = updatedEmployee.PhotoPath;
            }
            return employee;
        }
    }
}
