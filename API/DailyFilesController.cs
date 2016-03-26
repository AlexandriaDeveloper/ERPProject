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

        [HttpGet]
        public IHttpActionResult GetDetailsFile(int Id)
        {
            var details = _db.DailyFileDetailses.Find(Id);
            var detailsFile = _db.DailyFiles.Find(details.DailyFileId);
            var emp = _db.Employees.Find(details.EmployeeId);
            return Ok(new
            {
                dailyName = detailsFile.Name,
                dailyFileId = details.DailyFileId,
                empName = emp.Name,
                empId = emp.Id,
                net = details.Net

            });
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeInfo(int Id)
        {
            _db = new ERPContext();
            var empInfo = _db.DailyFileDetailses.Include("Employee").FirstOrDefault(x => x.Id == Id);
            if (empInfo == null)
            {
            }
            return Ok(empInfo);
        }
        [HttpPost]
        public IHttpActionResult UpdateEmpInfo(int DailyFileId, int EmployeeId, decimal Net)
        {

            var dailyfile = _db.DailyFiles.Find(DailyFileId);
            var empnum = _db.DailyFileDetailses.Count(x => x.DailyFileId == DailyFileId);

            if (dailyfile != null)
            {
                dailyfile.DailyFileDetailses = new List<DailyFileDetails>()
                {
                    new DailyFileDetails()
                    {
                        DailyFileId = DailyFileId,
                        EmployeeId = EmployeeId,
                        Net = Net
                    }
                };
                dailyfile.FileTotalAmount += Net;
                dailyfile.EmployeesNumber = empnum + 1;

                _db.SaveChanges();

                var root = HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/ATM.xls");

                string con1 =
    (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + root +
     ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");


                string con2 = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dailyfile.FilePath +
    ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");
                //string con2 =
                //  رقم الموظف بجهته الأصلية
                //string con2 = (@"provider=microsoft.ace.oledb.12.0;data source=c:\file\bank.xls;extended properties='excel 12.0 xml; hdr = yes; imex = 1';");
                var code = _db.Employees.Find(EmployeeId).Code;
                BL bl = new BL(con1);

                DataTable dtw = bl.GetTable(String.Format("select * from [Sheet1$] where [{0}] = '" +code.ToString()+"'", "رقم الموظف بجهته الأصلية"));
                BL bl2 = new BL(con2);

              DataTable dtw2=  bl2.GetTable(String.Format("select * from [Sheet1$] where [{0}] = '" + code.ToString() + "'", "رقم الموظف بجهته الأصلية"));

                if(dtw2.Rows.Count==0)
                { 
                bl2.Insert(String.Format("insert into [Sheet1$] values ('{0}','{1}','{2}','{3}','{4}','{5}',{6})"
                    , dtw.Rows[0].ItemArray[0]
                    , dtw.Rows[0].ItemArray[1]
                    , dtw.Rows[0].ItemArray[2]
                    , dtw.Rows[0].ItemArray[3]
                    , dtw.Rows[0].ItemArray[4]
                    , dtw.Rows[0].ItemArray[5]
                    ,Net
                    ));
                }
                else
                {
                    bl2.update(String.Format("update [Sheet1$] set[{1}] =[{1}]+" + Net + " where  [{0}] = '" + code.ToString() + "'",
                        "رقم الموظف بجهته الأصلية", "المرتب"));
                }
                ///		49	23706.61--145     73,984.58
                //if (EmpInfo != null)
                //{
                //    _db = new ERPContext();
                //    var model = _db.DailyFileDetailses.Find(EmpInfo.Id);
                //    model.Net = EmpInfo.Net;
                //    _db.SaveChanges();

                //}
                return Ok();
            }
            return BadRequest("Wrong Request");
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
                        var newname = CreateFileName(root);

                        if (provider.FileData.Count == 0)
                        {
                            File.Copy(HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/output.xls"), root + newname + ".xls", true);
                        }
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            //get Original File Path 
                            //   dailyFile.FilePath = file.LocalFileName;

                            // Move file from Original File Path to new Place
                            try
                            {
                                if (file.Headers.ContentDisposition.Size != 0)
                                {
                                    File.Move(file.LocalFileName, root + newname + ".xls");
                                }
                                else
                                {
                                    File.Copy(HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/output.xls"), root + newname + ".xls", true);
                                }
                            }
                            catch (Exception ex)
                            {

                                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                            }


                        }

                        InsertIntoDailyFiles(dailyFile, root, newname);


                        return Request.CreateResponse(HttpStatusCode.OK, files);

                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);

                    }

                }
                catch (Exception ex)
                {

                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
                }
            }




            return Request.CreateResponse(HttpStatusCode.OK, "success!");

        }

        private void InsertIntoDailyFiles(DailyFile dailyFile, string root, int newname)
        {
            dailyFile.FilePath = root + newname + ".xls";

            string con =
                (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dailyFile.FilePath +
                 ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            //string con2 =
            //      (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            BL bl = new BL(con);

            DataTable dtw = bl.GetTable("select * from [Sheet1$]");

       

            List<DailyFileDetails> list = new List<DailyFileDetails>();
            foreach (DataRow row in dtw.Rows)
            {
                if (row[6].ToString() != "")
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
            }

            dailyFile.EmployeesNumber = dailyFile.DailyFileDetailses.Count;
            dailyFile.FileTotalAmount = dailyFile.DailyFileDetailses.Sum(x => x.Net);
            _db.DailyFiles.Add(dailyFile);
            _db.SaveChanges();
        }

        private static int CreateFileName(string root)
        {
            int newname = 0;

            //Catch All Files Inside Source Folder
            string[] filesExist = Directory.GetFiles(root, "*.xls")
                .Select(path => Path.GetFileName(path))
                .ToArray();

            //Loop And Check The right File Name Can Be
            foreach (var filename in filesExist)
            {
                int t = 0;

                string namewithoutext = filename.Substring(0, filename.Length - 4);
                bool numericname = int.TryParse(namewithoutext, out t);
                if (t >= newname && numericname)

                    newname = t + 1;
            }
            return newname;
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

        [HttpDelete]
        public IHttpActionResult DeleteEmpInfo(int Id)
        {
            var found = _db.DailyFileDetailses.Include("DailyFile").Where(x => x.Id == Id).FirstOrDefault();
            var code = _db.Employees.Find(found.EmployeeId).Code;
            var empnum = _db.DailyFileDetailses.Count(x => x.DailyFileId == found.DailyFileId);
            if (found != null)
            {
                try
                {
                    string path = found.DailyFile.FilePath;
                    string con =
               (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");



                    BL bl = new BL(con);
                    bl.update(String.Format("update [Sheet1$] set[{1}] =[{1}]-"+found.Net.ToString()+" where  [{0}] = '" + code.ToString()+"'",
                        "رقم الموظف بجهته الأصلية", "المرتب"));


                    found.DailyFile.FileTotalAmount -= found.Net;
                    found.DailyFile.EmployeesNumber = empnum - 1;
                   
                    _db.DailyFileDetailses.Remove(found);
                    _db.SaveChanges();

                    
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw  new Exception(ex.Message);
                    return Conflict();
                }

            }
            return Conflict();
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
