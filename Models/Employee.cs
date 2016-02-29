using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
       
        public string Name { get; set; }
        public string  Nickname { get; set; }
        public int? Gender { get; set; }
  
        public string NationalId { get; set; }


        [Required]
        public int Code { get; set; }
      
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage = "تأكد من أدخال الأميل بشكل صحيح")]
        public string Email { get; set; }

        public bool Sallary { get; set; }
        public bool Other { get; set; }

        public Department Department { get; set; }
        public Position Position { get; set; }

        public virtual ICollection<DailyFileDetailsEmployee> DailyFileDetailsEmployee { get; set; }

        public Employee()
        {
            DailyFileDetailsEmployee = new List<DailyFileDetailsEmployee>();
        }
    }
}