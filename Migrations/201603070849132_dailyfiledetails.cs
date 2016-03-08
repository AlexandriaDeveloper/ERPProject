namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dailyfiledetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DailyFileDetailsEmployees", "DailyFileId", "dbo.DailyFileDetails");
            DropForeignKey("dbo.DailyFileDetailsEmployees", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.DailyFileDetailsEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.DailyFileDetailsEmployees", new[] { "DailyFileId" });
            AddColumn("dbo.DailyFileDetails", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.DailyFileDetails", "EmployeeId");
            AddForeignKey("dbo.DailyFileDetails", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            DropTable("dbo.DailyFileDetailsEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DailyFileDetailsEmployees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        DailyFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.DailyFileId });
            
            DropForeignKey("dbo.DailyFileDetails", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.DailyFileDetails", new[] { "EmployeeId" });
            DropColumn("dbo.DailyFileDetails", "EmployeeId");
            CreateIndex("dbo.DailyFileDetailsEmployees", "DailyFileId");
            CreateIndex("dbo.DailyFileDetailsEmployees", "EmployeeId");
            AddForeignKey("dbo.DailyFileDetailsEmployees", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DailyFileDetailsEmployees", "DailyFileId", "dbo.DailyFileDetails", "Id", cascadeDelete: true);
        }
    }
}
