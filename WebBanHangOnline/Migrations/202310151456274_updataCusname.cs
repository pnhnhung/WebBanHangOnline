namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataCusname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "CustumerName", c => c.String(nullable: false));
            DropColumn("dbo.tb_Order", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "CustomerName", c => c.String(nullable: false));
            DropColumn("dbo.tb_Order", "CustumerName");
        }
    }
}
