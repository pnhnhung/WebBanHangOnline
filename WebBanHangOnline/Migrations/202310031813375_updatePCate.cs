namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePCate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_ProductCategory", "Description", c => c.String());
            AlterColumn("dbo.tb_ProductCategory", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_ProductCategory", "Icon", c => c.String(nullable: false));
            AlterColumn("dbo.tb_ProductCategory", "Description", c => c.String(nullable: false));
        }
    }
}
