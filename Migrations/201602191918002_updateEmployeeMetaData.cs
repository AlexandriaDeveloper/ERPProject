namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmployeeMetaData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Phone", c => c.String(maxLength: 11));
            AddColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "NationalId", c => c.String(maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "NationalId", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Phone");
        }
    }
}
