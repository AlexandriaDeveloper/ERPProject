using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ERPProject.Models;
using ERPProject.ViewModel;

namespace ERPProject.API
{
    public class EmployeeController : ApiController
    {
        private ERPContext _db;



        //public IHttpActionResult Get()
        //{
        //    return Ok();
        //}
        
        public IHttpActionResult Get()
        {
            _db = new ERPContext();
           var employees = _db.Employees.AsQueryable();
         
          
            return Ok(employees.OrderBy(x=>x.Name));
        }
        [HttpGet()]
        public IHttpActionResult Get(int Id)
        {
            _db = new ERPContext();
            var result = _db.Employees.FirstOrDefault(x => x.Id == (Id));
            return  Ok(result);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]  Employee employee)
        {
            _db= new ERPContext();
            if (ModelState.IsValid)
            {

                if(employee.Id==0)
                _db.Employees.Add(employee);
               
            else
                {
                    var emp = _db.Employees.Find(employee.Id);
                    emp.Name = employee.Name;
                    emp.Nickname = employee.Nickname;
                    emp.Gender = employee.Gender;
                    emp.DepartmentId = employee.DepartmentId;
                    emp.PositionId = employee.PositionId;
                    emp.Phone = employee.Phone;
                    emp.Email = employee.Email;
                    emp.Code = employee.Code;
                    emp.NationalId = employee.NationalId;
                }
                _db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
           


        }
    }
}
