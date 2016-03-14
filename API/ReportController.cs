using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ERPProject.Models;

namespace ERPProject.API
{
    public class ReportController : ApiController
    {
        private ERPContext _db;
        public IHttpActionResult GetEmployees(int? Code = 0, string NationalId = "")
        {


            _db = new ERPContext();

             Employee empInfo = new Employee();

            if (Code > 0)
            {
                empInfo = _db.Employees.Include("DailyFileDetails.DailyFile.Daily").FirstOrDefault(x => x.Code == Code);
            }
            if (!string.IsNullOrEmpty(NationalId))
            {
                empInfo = _db.Employees.Include("DailyFileDetails.DailyFile.Daily").FirstOrDefault(x => x.NationalId == NationalId);
            }
            if (empInfo==null)
            {
                return NotFound();
            }

            return Ok(empInfo);
        }


    }
}
