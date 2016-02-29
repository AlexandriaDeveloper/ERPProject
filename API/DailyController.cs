using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
            var result = _db.Dailies.Include("ExpensessType").FirstOrDefault(x => x.Id == Id);

            return Ok(result);
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody] Daily model)
        {
            _db = new ERPContext();
            model.CreatedDate = DateTime.UtcNow;


            model.TotalAmount = 0;

            if (ModelState.IsValid)
            {

                if (model.Id == 0)
                {
                    model.Open = true;
                    _db.Dailies.Add(model);

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


                    }
                }
                _db.SaveChanges();
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
                return Ok(new {msg = "success"});
            }
            return BadRequest("Wrong Request");
        }

    }
}
