using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using ERPProject.Models;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
namespace ERPProject.API
{
    public class DailyArchiveController : ApiController
    {
        private ERPContext _db;

        public IHttpActionResult GetDailyArchives()
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;
            var result = _db.Dailies.Where(x => x.Open == false).Include("ExpensessType").OrderByDescending(x => x.ClosedDate);
            return Ok(result);

        }
        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            _db = new ERPContext();
            _db.Configuration.ProxyCreationEnabled = false;

            //var daily = _db.Dailies.Find(Id);
            //var dailyFiles = _db.DailyFiles.Where(x => x.DailyId == Id);
            var emp = _db.Employees;
            var dailydetails = _db.DailyFileDetailses.Where(x => x.DailyFile.DailyId == Id)
                .GroupBy(p => p.EmployeeId)
                .Select(g => new DailyDetailsInfoVM()
                {
                    Id = g.Key,
                    Name = emp.FirstOrDefault(x => x.Id == g.Key).Name,
                    Details = g.Select(x => x.DailyFile).Count().ToString(),
                    Description = g.Select(x => x.DailyFile),
                    Code = emp.FirstOrDefault(x => x.Id == g.Key).Code,
                    Net = g.Sum(i => i.Net)
                }).ToList();
            return Ok(dailydetails);
        }

        private string filPath = "";

        [HttpGet]
        public IHttpActionResult ExportExcel(int Id)
        {
            _db = new ERPContext();
            var emp = _db.Employees;
            var dailydetails = _db.DailyFileDetailses.Where(x => x.DailyFile.DailyId == Id)
                .GroupBy(p => p.EmployeeId)
                .Select(g => new DailyDetailsInfoVM
                {
                    Id = g.Key,
                    Name = emp.FirstOrDefault(x => x.Id == g.Key).Name,

                    Code = emp.FirstOrDefault(x => x.Id == g.Key).Code,
                    Net = g.Sum(i => i.Net)
                }).ToList();

         //   Excel.Application oApp = new Excel.Application();

            string filePath = HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/output.xls");
            File.Copy(filePath, HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/output2.xls").ToString(), true);

            filePath = HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/output2.xls");
            //  Excel.Workbook oBook = oApp.Workbooks.Open(filePath);
            //  Excel.Worksheet oSheet = (Excel.Worksheet)oBook.Sheets.get_Item(1);
            //Excel.Range  oRange = oSheet.Cells[2, 1] as Excel.Range;

            string filePath2 = HttpContext.Current.Server.MapPath("~/Uploads/SourceFile/ATM.xls");
            //Excel.Workbook oBook2 = oApp.Workbooks.Open(filePath2);
            //Excel.Worksheet oSheet2 = (Excel.Worksheet)oBook2.Sheets.get_Item(1);
            //object misValue = System.Reflection.Missing.Value;
            BL bl = new BL(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + filePath2 + "; Extended Properties = Excel 12.0");
            DataTable dt = new DataTable();

            dt = bl.GetTable("select * from [Sheet1$]");
            dt.PrimaryKey = new DataColumn[] { dt.Columns[4] };
            BL b2 =
                new BL(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + filePath +
                        "; Extended Properties = Excel 12.0");
            DataTable dt2 = new DataTable();
            foreach (var row in dailydetails)
            {
                //  if(row.Code.ToString()== oSheet.Rows.Find(row.Code))
                var found = dt.Rows.Find(row.Code).ItemArray;

                b2.Insert(string.Format("insert into [Sheet1$] values('{0}','{1}','{2}','{3}','{4}','{5}',{6})",
                    found[0], found[1], found[2], found[3], found[4], found[5], row.Net));


            }
            //   HttpResponseMessage result = null;


            //byte[] bytes = Convert.FromBase64String(File.r;


            //result = Request.CreateResponse(HttpStatusCode.OK);
            //result.Content = new ByteArrayContent(bytes);
            //result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //result.Content.Headers.ContentDisposition.FileName = file.name + ".pdf";






            //     MemoryStream ms = new MemoryStream();
           



          




            return Ok(new {result= filePath });
        }


        [HttpGet]
        public HttpResponseMessage ExportExcel2(string filePath)
        {
            byte[] bytes = null;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                BinaryReader br = new BinaryReader(file);

                bytes = br.ReadBytes((int)file.Length);
                br.Close();

                //   file.Read(bytes, 0,(int) file.Length);
            }
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            MemoryStream stream = new MemoryStream(bytes);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "output2.xls"
            };

    
            return result;
        }
    }


    public class DailyDetailsInfoVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public dynamic Description { get; set; }
        public int Code { get; set; }
        public decimal Net { get; set; }
    }
}
