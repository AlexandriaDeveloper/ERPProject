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
            string con =
                (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\ATM.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            string con2 =
                  (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\File\Bank.xls;Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1';");

            BL bl = new BL(con);

            DataTable dtw = bl.GetTable("select * from [Sheet1$]");
            _db = new ERPContext();
            foreach (DataRow row in dtw.Rows)
            {
                _db.Employees.Add(new Employee()
                {
                    NationalId = row[0].ToString(),
                    Name = row[5].ToString(),
                    Code = Convert.ToInt32( row[4]),
                    Sallary = true,
                    Other = true


                });
                _db.SaveChanges();
            }
      

            return Ok();
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