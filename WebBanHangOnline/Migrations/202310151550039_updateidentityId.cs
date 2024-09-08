namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateidentityId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AlterColumn("dbo.tb_OrderDetail", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AlterColumn("dbo.tb_OrderDetail", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "Id", "ProductId" });
        }
    }
}
