using System.Data.OleDb;

namespace ERPProject.API
{
    public class DAL
    {
        public OleDbConnection con { get; set; }

        public DAL(string con)
        {
            //initilize new DbConnection depending on Con strin
            this.con = new OleDbConnection(con);

        }
    }
}