using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using ERPProject.Models;
using Newtonsoft.Json;

namespace ERPProject.API
{
    public class DailyArchiveController : ApiController
    {
        private ERPContext _db;
       
        public IHttpActionResult GetDailyArchives()
        {_db= new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result= _db.Dailies.Where(x => x.Open == false).OrderByDescending(x =>x.ClosedDate);
            return Ok(result);
        }
        [HttpGet]
        public IHttpActionResult Get(int Id)
        {_db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            
            //var daily = _db.Dailies.Find(Id);
            //var dailyFiles = _db.DailyFiles.Where(x => x.DailyId == Id);
            var emp = _db.Employees;
            var dailydetails = _db.DailyFileDetailses.Where(x=>x.DailyFile.DailyId== Id)
                .GroupBy(p => p. EmployeeId)
                .Select(g => new DailyDetailsInfoVM()
                {
                    Id = g.Key,
                    Name=emp.Where   (x=>x.Id==g.Key).FirstOrDefault().Name,
                  Code = emp.Where(x => x.Id == g.Key).FirstOrDefault().Code,
                    Net = g.Sum(i => i.Net)
                }).ToList();
            return Ok(dailydetails);
        }


        [HttpGet]
        public IHttpActionResult ExportExcel(int Id)
        {
            _db = new ERPContext();
            var emp = _db.Employees;
            var dailydetails = _db.DailyFileDetailses.Where(x => x.DailyFile.DailyId == Id)
                .GroupBy(p => p.EmployeeId)
                .Select(g => new DailyDetailsInfoVM()
                {
                    Id = g.Key,
                    Name = emp.Where(x => x.Id == g.Key).FirstOrDefault().Name,
                    Code = emp.Where(x => x.Id == g.Key).FirstOrDefault().Code,
                    Net = g.Sum(i => i.Net)
                }).ToList();

            return Ok();
        }
    }


    public class DailyDetailsInfoVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public decimal Net { get; set; }
    }
}
