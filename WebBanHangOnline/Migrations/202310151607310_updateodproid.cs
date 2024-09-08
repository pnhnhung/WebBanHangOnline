namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateodproid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tb_OrderDetail", new[] { "ProductId" });
            DropPrimaryKey("dbo.tb_OrderDetail");
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
            CreateIndex("dbo.tb_OrderDetail", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tb_OrderDetail", new[] { "ProductId" });
            DropPrimaryKey("dbo.tb_OrderDetail");
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
            CreateIndex("dbo.tb_OrderDetail", "ProductId");
        }
    }
}
