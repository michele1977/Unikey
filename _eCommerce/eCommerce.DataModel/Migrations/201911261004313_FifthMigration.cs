namespace eCommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID", cascadeDelete: true);
            DropColumn("dbo.Products", "IdBrand");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IdBrand", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int());
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID");
        }
    }
}
