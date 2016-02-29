using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class DailyFileDetailsEmployee
    {
     
        public int DailyFileId { get; set; }
      
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DailyFileDetails DailyFileDetails { get; set; }
    }
}