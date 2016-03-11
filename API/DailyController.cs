using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.Http;
using ERPProject.Models;
using Newtonsoft.Json;

namespace ERPProject.API
{
    public class DailyController : ApiController
    {
        private ERPContext _db;




        public IHttpActionResult Get()
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result = _db.Dailies.Include("ExpensessType").OrderByDescending(x => x.CreatedDate).Where(x => x.Open).ToList();

            return Ok(result);
        }
        public IHttpActionResult Get(int Id)
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result = _db.Dailies
                .Include("ExpensessType")
                .Include("DailyFiles")
                .Where(x=>x.Open)
                .FirstOrDefault(x => x.Id == Id);


            List<DailyFile> resultfiles = _db.DailyFiles
                .Where(x => x.DailyId == Id).ToList();
            decimal totalamount=0;

            //foreach (DailyFile data in resultfiles)
            //{
            //   totalamount = data.DailyFileDetailses.Where(x => x.DailyFileId == data.Id).Sum(x => x.Net);
            //}

         
            return Ok(new
            {
                result = result,
                 totalamount=totalamount
            });
        }

        public IHttpActionResult GetClosed(int Id)
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result = _db.Dailies
                .Include("ExpensessType")
                .Include("DailyFiles")
                .Where(x => x.Open==false)
                .FirstOrDefault(x => x.Id == Id);


            List<DailyFile> resultfiles = _db.DailyFiles
                .Where(x => x.DailyId == Id).ToList();
            decimal totalamount = 0;

            //foreach (DailyFile data in resultfiles)
            //{
            //   totalamount = data.DailyFileDetailses.Where(x => x.DailyFileId == data.Id).Sum(x => x.Net);
            //}


            return Ok(new
            {
                result = result,
                totalamount = totalamount
            });
        }
        public IHttpActionResult GetParent(int Id)
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result = _db.Dailies
                .Include("ExpensessType")
                .Include("DailyFiles")
               
                .FirstOrDefault(x => x.Id == Id);


            List<DailyFile> resultfiles = _db.DailyFiles
                .Where(x => x.DailyId == Id).ToList();
            decimal totalamount = 0;

            //foreach (DailyFile data in resultfiles)
            //{
            //   totalamount = data.DailyFileDetailses.Where(x => x.DailyFileId == data.Id).Sum(x => x.Net);
            //}


            return Ok(new
            {
                result = result,
                totalamount = totalamount
            });
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Daily model)
        {
            _db = new ERPContext();
         


            model.TotalAmount = 0;
           

            if (ModelState.IsValid)
            {

                if (model.Id == 0)
                {
                    model.Open = true;
                    model.CreatedDate = DateTime.UtcNow;
                    _db.Dailies.Add(model);
                    _db.SaveChanges();
                    var filePath =
                      HttpContext.Current.Server.MapPath("~/Uploads/DailyFiles/Daily-" + model.Id);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                }
                else
                {
                    var daily = _db.Dailies.Find(model.Id);
                    if (daily != null)
                    {
                        daily.Open = model.Open;
                        daily.Name = model.Name;
                        daily.ClosedDate = model.ClosedDate;
                        daily.CheckGP = model.CheckGP;
                        daily.DailyDay = model.DailyDay;
                        daily.ExpensessTypeId = model.ExpensessTypeId;
                        _db.SaveChanges();

                    }
                }

   


            }
            return Ok(new { msg = "success" });
        }


        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {

            _db = new ERPContext();
            var daily = _db.Dailies.Find(Id);


            if (daily != null)
            {
                _db.Dailies.Remove(daily);
                _db.SaveChanges();

                var filePath =
                       HttpContext.Current.Server.MapPath("~/Uploads/DailyFiles/Daily-" + Id);


                if (Directory.Exists(filePath))
                {
                    try
                    {
                        Directory.Delete(filePath, true);
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(0);
                        Directory.Delete(filePath, true);
                    }
                }
                return Ok(new { msg = "success" });
            }
            return BadRequest("Wrong Request");
        }

    }
}
