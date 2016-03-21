using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using ERPProject.Models;
using ERPProject.ViewModel;
using Newtonsoft.Json;
using ERPProject.Models;
using Newtonsoft.Json;

namespace ERPProject.API
{
    public class FileUploadController : ApiController
    {
        private ERPContext _db;
        // GET: FileUpload
        //  [MimeMultipart]

        public IHttpActionResult GetFormData()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult PostFormData()
        {

            var httpRequest = HttpContext.Current.Request;
            string path = "";
            string filePath = "";


            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        long dataSourceId = 1;
                        var postedFile = httpRequest.Files[file];
                        filePath = HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/" + postedFile.FileName);
                        //postedFile.SaveAs(filePath);
                        Stream stream = postedFile.InputStream;
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(postedFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                        }
                        var strrr = new MemoryStream(fileData);
                        var tempfile = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                        // Get random file name without 
                        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            // Write content of your memory stream into file stream
                            strrr.WriteTo(fs);
                        }                      
                    }
                }


                string check = "ATM";
                string con =
                    (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

          
                BL bl = new BL(con);

                DataTable dtw = bl.GetTable("select * from [Sheet1$]");
                _db = new ERPContext();
                var employees = _db.Employees.ToList();


               var payingType= dtw.Rows[1].ItemArray[1];
                bool pay = true;
                if (payingType.Equals("3-مرتب تحويلات بنكية"))
                {
                    check = "Bank";
                    pay = false;
                }
                foreach (DataRow row in dtw.Rows)
                {

                 //   Employee emp = (from c in employees where (c.Code == Convert.ToInt32(row[4])) select (c)).FirstOrDefault();
                    int code=0;
                    bool codeparse = int.TryParse(row[4].ToString(), out code);

                    Employee emp = null;
                    if (codeparse)
                    {  emp = _db.Employees.FirstOrDefault(x => x.Code == code);
                    }
                   
                    if (emp != null)

                    {
                        emp.Sallary = pay;
                        emp.Name = row[5].ToString();
                        emp.NationalId = row[0].ToString();

                    }
                    else
                    {
                        Employee empl = new Employee
                        {
                            NationalId = row[0].ToString(),
                            Name = row[5].ToString(),
                            Code = Convert.ToInt32(row[4]),
                            Sallary = pay,
                            Other = true
                        };



                        _db.Employees.Add(empl);
                    }
                  

                          
                }
               _db.SaveChanges();
                _db.Dispose();
                if (File.Exists(HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/" + check + ".xls")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/" + check + ".xls"));
                }

                File.Move(filePath, HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/" + check + ".xls"));

                return Ok(new { msg = "success" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

           
        }

    }
}