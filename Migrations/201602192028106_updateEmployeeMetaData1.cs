namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmployeeMetaData1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "NationalId", c => c.String());
            AlterColumn("dbo.Employees", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Phone", c => c.String(maxLength: 11));
            AlterColumn("dbo.Employees", "NationalId", c => c.String(maxLength: 14));
        }
    }
}
