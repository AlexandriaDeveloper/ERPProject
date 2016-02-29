using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Web.Http;
using ERPProject.Models;

namespace ERPProject.API
{

    public class ExpensessTypeController : ApiController
    {
        private ERPContext _db;



        public IHttpActionResult GetExpensessType()
        {
            _db = new ERPContext();

            var result = (from c in _db.ExpensessTypes
                          select new
                          {
                              value = c.Id,
                              label = c.Name
                          }).ToList();

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Error");

        }
    }
}
