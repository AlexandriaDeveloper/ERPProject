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
                     filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + postedFile.FileName);
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
                     if (row[1].ToString() == "3-مرتب تحويلات بنكية")
                    {
                        emp.Sallary = false;
                    }
                    else
                    {
                        emp.Sallary = true;
                    }

                if (emp != null)

                {

               

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



    public class DAL
    {
        public OleDbConnection con { get; set; }

        public DAL(string con)
        {
            //initilize new DbConnection depending on Con strin
            this.con = new OleDbConnection(con);

        }
    }

    public class BL : DAL
    {
        private DataTable dt;
        private OleDbCommand cmd;

        /// <summary>
        /// construct new DbConnection 
        /// initilize table container - Command- link command with initilized Connection
        /// </summary>
        /// <param name="con"></param>
        public BL(string con)
            : base(con)
        {
            dt = new DataTable();
            cmd = new OleDbCommand();
            cmd.Connection = this.con;
        }
        /// <summary>
        /// this method used for geting List of All Sheets
        /// </summary>
        /// <returns></returns>
        public string[] WorkbookSheets()
        {
            //clear Table
            dt.Clear();

            try
            {
                using (con)
                {
                    //check Connection
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    //get Schema
                    dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    var excelSheets = new String[dt.Rows.Count];
                    var i = 0;

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        i++;
                    }
                    return excelSheets;
                }
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message);
                return null;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable GetTable(string text)
        {
            //clear table 
            dt.Clear();

            using (cmd = con.CreateCommand())
            {
                cmd.CommandText = text;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //fill Table 

                try
                {
                    OleDbDataAdapter adb = new OleDbDataAdapter();
                    adb.SelectCommand = cmd;
                    adb.Fill(dt);
                }
                catch (Exception ex)
                {

                    //   MessageBox.Show(ex.Message.ToString());
                }

                finally
                {
                    con.Close();
                }


            }
            //return Table
            return dt;
        }
        /// <summary>
        /// This Method Used For 
        /// Insert New Value or Row
        /// </summary>
        /// <param name="text"></param>
        public void Insert(string text)
        {

            using (cmd = con.CreateCommand())
            {
                cmd.CommandText = text;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                OleDbDataAdapter adb = new OleDbDataAdapter();
                adb.InsertCommand = cmd;
                try
                {
                    adb.InsertCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                    // MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }

        }



        }

    }