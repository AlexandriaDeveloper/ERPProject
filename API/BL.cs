using System;
using System.Data;
using System.Data.OleDb;

namespace ERPProject.API
{
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