namespace eCommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "IdBrand", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "IdBrand" });
            RenameColumn(table: "dbo.Products", name: "IdBrand", newName: "Brand_ID");
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int());
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            AlterColumn("dbo.Products", "Brand_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Products", name: "Brand_ID", newName: "IdBrand");
            CreateIndex("dbo.Products", "IdBrand");
            AddForeignKey("dbo.Products", "IdBrand", "dbo.Brands", "ID", cascadeDelete: true);
        }
    }
}
