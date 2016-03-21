namespace ERPProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDailyFileNumberToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyFiles", "FileNumberInfo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyFiles", "FileNumberInfo", c => c.Int());
        }
    }
}
