using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class DailyFileDetails
    {
        public int Id { get; set; }
    
        public decimal Net { get; set; }
        public int DailyFileId { get; set; }
        public DailyFile DailyFile { get; set; }

        public virtual ICollection<DailyFileDetailsEmployee> DailyFileDetailsEmployee { get; set; }

        public DailyFileDetails()
        {
            DailyFileDetailsEmployee = new List<DailyFileDetailsEmployee>();
        }
       
    }
}