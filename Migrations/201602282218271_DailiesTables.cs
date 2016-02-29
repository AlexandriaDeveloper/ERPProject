namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailiesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dailies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TotalAmount = c.Decimal(precision: 18, scale: 2),
                        CheckGP = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DailyDay = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        ExpensessTypeId = c.Int(nullable: false),
                        FilePath = c.String(),
                        Open = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpensessTypes", t => t.ExpensessTypeId, cascadeDelete: true)
                .Index(t => t.ExpensessTypeId);
            
            CreateTable(
                "dbo.DailyFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FileNumberInfo = c.Int(),
                        EmployeesNumber = c.Int(),
                        FileTotalAmount = c.Decimal(precision: 18, scale: 2),
                        FilePath = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DailyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dailies", t => t.DailyId, cascadeDelete: true)
                .Index(t => t.DailyId);
            
            CreateTable(
                "dbo.DailyFileDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Net = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DailyFiles", t => t.DailyFileId, cascadeDelete: true)
                .Index(t => t.DailyFileId);
            
            CreateTable(
                "dbo.DailyFileDetailsEmployees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        DailyFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.DailyFileId })
                .ForeignKey("dbo.DailyFileDetails", t => t.DailyFileId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DailyFileId);
            
            CreateTable(
                "dbo.ExpensessTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dailies", "ExpensessTypeId", "dbo.ExpensessTypes");
            DropForeignKey("dbo.DailyFiles", "DailyId", "dbo.Dailies");
            DropForeignKey("dbo.DailyFileDetails", "DailyFileId", "dbo.DailyFiles");
            DropForeignKey("dbo.DailyFileDetailsEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DailyFileDetailsEmployees", "DailyFileId", "dbo.DailyFileDetails");
            DropIndex("dbo.DailyFileDetailsEmployees", new[] { "DailyFileId" });
            DropIndex("dbo.DailyFileDetailsEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.DailyFileDetails", new[] { "DailyFileId" });
            DropIndex("dbo.DailyFiles", new[] { "DailyId" });
            DropIndex("dbo.Dailies", new[] { "ExpensessTypeId" });
            DropTable("dbo.ExpensessTypes");
            DropTable("dbo.DailyFileDetailsEmployees");
            DropTable("dbo.DailyFileDetails");
            DropTable("dbo.DailyFiles");
            DropTable("dbo.Dailies");
        }
    }
}
