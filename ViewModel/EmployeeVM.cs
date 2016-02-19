using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using ERPProject.Models;

namespace ERPProject.ViewModel
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }

        public List<Department> Departments { get; set; }
        public int SelectedDepartment { get; set; }

        public EmployeeVM()
        {
            Departments= new List<Department>();
        }

    }

    public class EmployeeSearch
    {
        public string Name { get; set; }
        public string NationalId { get; set; }
        public int DepartmentId { get; set; }
        
    }

}