using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ERPProject.Models;
using Newtonsoft.Json;

namespace ERPProject.API
{
    public class DailyFilesController : ApiController
    {
        private ERPContext _db = new ERPContext();

        // GET: api/DailyFiles
        public IQueryable<DailyFile> GetDailyFiles(int Id)
        {

            _db = new ERPContext();

            _db.Configuration.ProxyCreationEnabled = false;


            var result = _db.DailyFiles.OrderByDescending(x => x.CreatedDate).Where(x => x.DailyId == Id).AsQueryable();


            return result;
        }

        // GET: api/DailyFiles/5
        //  [ResponseType(typeof(DailyFile))]
        [HttpGet]
        public IHttpActionResult GetDailyFile(int Id)
        {
            DailyFile dailyFile = _db.DailyFiles.Find(Id);
            dailyFile.DailyFileDetailses = new List<DailyFileDetails>();
            dailyFile.DailyFileDetailses =
                _db.DailyFileDetailses.Where(x => x.DailyFileId == Id).Include("Employee").ToList();

            if (dailyFile == null)
            {
                return NotFound();
            }

            return Ok(dailyFile);
        }

        // PUT: api/DailyFiles/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutDailyFile(DailyFile dailyFile)
        {

            int id = dailyFile.Id;
            dailyFile.CreatedDate = DateTime.UtcNow;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyFile.Id)
            {
                return BadRequest();
            }

            _db.Entry(dailyFile).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyFileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DailyFiles

        [HttpPost]
        public async Task<HttpResponseMessage> PostDailyFile(int Id)
        {

            if (Request.Content.IsMimeMultipartContent())
            {
                try
                {






                    // Get Destination Folder Path
                    var root = HttpContext.Current.Server.MapPath("~/Uploads/DailyFiles/Daily-" + Id.ToString() + "/");



                    //get provider 
                    CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(root);

                    List<string> files = new List<string>();

                    try
                    {
                        await Request.Content.ReadAsMultipartAsync(provider);

                        //Fetch Normal Data
                        var model = provider.FormData["model"];

                        //Fetch Files Inside
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            files.Add(Path.GetFileName(file.LocalFileName));
                        }




                        //Desirilze Json Data Object 
                        var dailyFile = JsonConvert.DeserializeObject<DailyFile>(model);

                        //Insert Creation Date
                        dailyFile.CreatedDate = DateTime.UtcNow;

                        if (!ModelState.IsValid)
                        {
                            return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                        }
                        //Files Name Counter 
                        int newname = 0;
                        //Catch All Files Inside Source Folder
                        string[] filesExist = Directory.GetFiles(root,"*.xls")
                            .Select(path => Path.GetFileName(path))
                                     .ToArray(); ;
                        //Loop And Check The right File Name Can Be
                        foreach (var filename in filesExist)
                        {
                            int t=0;
                            bool numericname = false;
                            string namewithoutext = filename.Substring(0,filename.Length - 4);
                            numericname = int.TryParse(namewithoutext,out t);
                            if (numericname)
                                newname++;
                        }


                        foreach (MultipartFileData file in provider.FileData)
                        {
                            //get Original File Path 
                            //   dailyFile.FilePath = file.LocalFileName;

                            // Move file from Original File Path to new Place
                            try
                            {
                                File.Move(file.LocalFileName, root + newname + ".xls");
                            }
                            catch (Exception ex )
                            {

                                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                            }
                           

                        }
                     
                        dailyFile.FilePath = root + newname + ".xls";

                        string con =
               (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dailyFile.FilePath + ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

                        //string con2 =
                        //      (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

                        BL bl = new BL(con);

                        DataTable dtw = bl.GetTable("select * from [Sheet1$]");

                        List<DailyFileDetails> list = new List<DailyFileDetails>();
                        foreach (DataRow row in dtw.Rows)
                        {
                            int empid = Convert.ToInt32(row[4]);

                            if (Convert.ToDecimal(row[6]) > 0)
                            {
                                var firstOrDefault = _db.Employees.FirstOrDefault(x => x.Code == (empid));
                                if (firstOrDefault != null)
                                    dailyFile.DailyFileDetailses.Add(new DailyFileDetails()
                                    {
                                        Net = Convert.ToDecimal(row[6]),
                                        EmployeeId = firstOrDefault.Id,
                                        DailyFileId = dailyFile.Id,
                                        Employee = firstOrDefault

                                    });
                            }
                        }
                        dailyFile.EmployeesNumber = dailyFile.DailyFileDetailses.Count;
                        dailyFile.FileTotalAmount = dailyFile.DailyFileDetailses.Sum(x => x.Net);
                        _db.DailyFiles.Add(dailyFile);
                        _db.SaveChanges();



                        return Request.CreateResponse(HttpStatusCode.OK, files);

                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                        //, return Request.CreateErrorResponse(HttpStatusCode.InternalServerError);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }










            //var httpRequest = HttpContext.Current.Request;
            //var root = HttpContext.Current.Server.MapPath("~/Uploads/DailyFiles");
            //var provider = new MultipartFormDataStreamProvider(root);
            //var model=  provider.FormData["model"];
            //var files = provider.FormData["files"];
            ////var result =await  Request.Content.ReadAsMultipartAsync(provider);
            ////var model = result.FormData["model"];
            ////var files = result.FormData["files"];

            //foreach (var file in provider.FileData)
            //{
            //    //TODO: Do something with each uploaded file
            //}
            return Request.CreateResponse(HttpStatusCode.OK, "success!");
            //dailyFile.CreatedDate = DateTime.UtcNow;
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //_db.DailyFiles.Add(dailyFile);
            //_db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = dailyFile.Id }, dailyFile);
        }

        // DELETE: api/DailyFiles/5
        [HttpDelete]
        public IHttpActionResult DeleteDailyFile(int Id)
        {
            DailyFile dailyFile = _db.DailyFiles.Find(Id);

            if (dailyFile == null)
            {
                return NotFound();
            }

            _db.DailyFiles.Remove(dailyFile);
            if (File.Exists(dailyFile.FilePath))
            {
                File.Delete(dailyFile.FilePath);
            }
            _db.SaveChanges();

            return Ok(dailyFile);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyFileExists(int id)
        {
            return _db.DailyFiles.Count(e => e.Id == id) > 0;
        }
    }
}
