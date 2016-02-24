namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEmployeeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Sallary", c => c.Boolean(nullable: false,defaultValue:true));
            AddColumn("dbo.Employees", "Other", c => c.Boolean(nullable: false, defaultValue: true)
             );
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Other");
            DropColumn("dbo.Employees", "Sallary");
        }
    }
}
