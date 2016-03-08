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
            //string con =
            //  (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");



            //BL bl = new BL(con);

            //DataTable dtw = bl.GetTable("select * from [Sheet1$]");
            //_db = new ERPContext();
            //foreach (DataRow row in dtw.Rows)
            //{
            //    _db.Employees.Add(new Employee()
            //    {
            //        NationalId = row[0].ToString(),
            //        Name = row[5].ToString(),
            //        Code = (int)row[4]


            //    });
            //}
            //_db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult PostFormData()
        {

            var httpRequest = HttpContext.Current.Request;
            string path = "";
            string filePath = "";
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



                    var temp = @"c:\File"; // Get %TEMP% path
                   

                    var tempfile = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()); // Get random file name without extension
                    var ext = Path.GetExtension(tempfile);
                     path = Path.Combine(temp, postedFile.FileName); // Get random file path

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        // Write content of your memory stream into file stream
                       strrr.WriteTo(fs);
                    }

                    // Create Excel app
                 //   Excel.Application excel = new Excel.Application();

                    // Open Workbook
                  //  excel.Workbooks.Open(path, ReadOnly: true);

                   // StreamWriter stw = new StreamWriter(strrr,Encoding.Unicode);
                }






            }



            string con =
                (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ filePath + ";Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            //string con2 =
            //      (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            BL bl = new BL(con);

            DataTable dtw = bl.GetTable("select * from [Sheet1$]");
            _db = new ERPContext();
            var employees = _db.Employees.ToList();
            foreach (DataRow row in dtw.Rows)
            {

             Employee   emp = (from c in employees where (c.Code == Convert.ToInt32(row[4])) select (c)).FirstOrDefault();
           
                if (emp != null)

                {

                    if (row[1].ToString() == "3-مرتب تحويلات بنكية")
                    {
                        emp.Sallary = false;
                    }
                    else
                    {
                        emp.Sallary = true;
                    }


                    //  emp.Other = row[1].ToString() != "3-مرتب تحويلات بنكية";
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
                      
                        Other = true
                    };



                    _db.Employees.Add(empl);
                }


                _db.SaveChanges();
            }


            return Ok(new {msg="success"});
        }


        //public string DataTableToJSONWithJSONNet(DataTable table)
        //{
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(table);
        //    return JSONString;
        //}


    }
}