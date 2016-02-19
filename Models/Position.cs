using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPProject.Models
{
    public class Position
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Employee> Employees { get; set; }

        public Position()
        {
            Employees= new List<Employee>();
        }
    }
}