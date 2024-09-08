namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updat124 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
        }
    }
}
