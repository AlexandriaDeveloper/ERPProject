using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ERPProject.Models;
using ERPProject.ViewModel;
using Newtonsoft.Json;

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
            _db.Configuration.ProxyCreationEnabled = false;
            var employees = _db.Employees.Include("Department").Include("Position").AsQueryable();
            return Ok(employees.OrderBy(x => x.Name));
        }

        [HttpGet()]
        public IHttpActionResult Get(int Id)
        {
            _db = new ERPContext();
            var result = _db.Employees.FirstOrDefault(x => x.Id == (Id));
            return Ok(result);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            _db = new ERPContext();
            if (ModelState.IsValid)
            {

                if (employee.Id == 0)
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
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }



        }



        //[HttpPost]
        //public IHttpActionResult Post(HttpRequestMessage file,int id= 0)
        //{
        //    return Ok("");
        //}


        [ResponseType(typeof (Employee))]

        public IHttpActionResult DeleteEmployee(int entityId)
        {
            _db = new ERPContext();
            Employee employee = _db.Employees.Find(entityId);
            if (employee == null)
            {
                return NotFound();
            }

            _db.Employees.Remove(employee);
            _db.SaveChanges();
            //string con =
            //    (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");



            //BL bl = new BL(con);

            //DataTable dtw = bl.GetTable("select * from [Sheet1$]");
            //_db = new ERPContext();
            //foreach (DataRow row in dtw.Rows)
            //{

            //    Employee EMP = new Employee();
            //    EMP.NationalId = row[0].ToString();
            //    EMP.Name = row[5].ToString();
            //    EMP.Code = Convert.ToInt32( row[4]);
            //    _db.Employees.Add(EMP);
            //    _db.SaveChanges();
            //}

            return Ok(new {msg = "Delete Successfully"});
        }


    }


}
