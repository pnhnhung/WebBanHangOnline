namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1116 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", "Id");
        }
    }
}
