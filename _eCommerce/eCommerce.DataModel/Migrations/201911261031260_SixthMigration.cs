namespace eCommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Brand_ID", newName: "IdBrand");
            RenameIndex(table: "dbo.Products", name: "IX_Brand_ID", newName: "IX_IdBrand");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_IdBrand", newName: "IX_Brand_ID");
            RenameColumn(table: "dbo.Products", name: "IdBrand", newName: "Brand_ID");
        }
    }
}
