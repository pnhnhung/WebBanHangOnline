namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1210 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Product", "ProductCategory_Id", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Product", new[] { "ProductCategory_Id" });
            RenameColumn(table: "dbo.tb_Product", name: "ProductCategory_Id", newName: "ProductCategoryID");
            AlterColumn("dbo.tb_Product", "ProductCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Product", "ProductCategoryID");
            AddForeignKey("dbo.tb_Product", "ProductCategoryID", "dbo.tb_ProductCategory", "Id", cascadeDelete: true);
            DropColumn("dbo.tb_Product", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "CategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_Product", "ProductCategoryID", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Product", new[] { "ProductCategoryID" });
            AlterColumn("dbo.tb_Product", "ProductCategoryID", c => c.Int());
            RenameColumn(table: "dbo.tb_Product", name: "ProductCategoryID", newName: "ProductCategory_Id");
            CreateIndex("dbo.tb_Product", "ProductCategory_Id");
            AddForeignKey("dbo.tb_Product", "ProductCategory_Id", "dbo.tb_ProductCategory", "Id");
        }
    }
}
