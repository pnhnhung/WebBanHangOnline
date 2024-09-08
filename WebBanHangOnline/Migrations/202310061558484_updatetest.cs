namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategoryProducts", "ProductCategory_Id", "dbo.tb_ProductCategory");
            DropForeignKey("dbo.ProductCategoryProducts", "Product_Id", "dbo.tb_Product");
            DropForeignKey("dbo.tb_ProductImgs", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_ProductImgs", new[] { "ProductId" });
            DropIndex("dbo.ProductCategoryProducts", new[] { "ProductCategory_Id" });
            DropIndex("dbo.ProductCategoryProducts", new[] { "Product_Id" });
            AddColumn("dbo.tb_Product", "ProductCategory_Id", c => c.Int());
            CreateIndex("dbo.tb_Product", "ProductCategory_Id");
            AddForeignKey("dbo.tb_Product", "ProductCategory_Id", "dbo.tb_ProductCategory", "Id");
            DropTable("dbo.tb_ProductImgs");
            DropTable("dbo.ProductCategoryProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategoryProducts",
                c => new
                    {
                        ProductCategory_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_Id, t.Product_Id });
            
            CreateTable(
                "dbo.tb_ProductImgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        isDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.tb_Product", "ProductCategory_Id", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Product", new[] { "ProductCategory_Id" });
            DropColumn("dbo.tb_Product", "ProductCategory_Id");
            CreateIndex("dbo.ProductCategoryProducts", "Product_Id");
            CreateIndex("dbo.ProductCategoryProducts", "ProductCategory_Id");
            CreateIndex("dbo.tb_ProductImgs", "ProductId");
            AddForeignKey("dbo.tb_ProductImgs", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategoryProducts", "Product_Id", "dbo.tb_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategoryProducts", "ProductCategory_Id", "dbo.tb_ProductCategory", "Id", cascadeDelete: true);
        }
    }
}
