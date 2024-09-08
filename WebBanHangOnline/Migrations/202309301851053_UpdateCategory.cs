namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Category", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Category", "Title", c => c.String());
        }
    }
}
