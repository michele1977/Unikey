namespace eCommerce.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "Weigth", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "IdBrand", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Brand_ID", c => c.Int());
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            CreateIndex("dbo.Products", "Brand_ID");
            AddForeignKey("dbo.Products", "Brand_ID", "dbo.Brands", "ID");
            DropColumn("dbo.Products", "Name");
            DropColumn("dbo.Products", "Category");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Category", c => c.String());
            AddColumn("dbo.Products", "Name", c => c.String());
            DropForeignKey("dbo.Products", "Brand_ID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Brand_ID" });
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.Products", "Brand_ID");
            DropColumn("dbo.Products", "IdBrand");
            DropColumn("dbo.Products", "Weigth");
            DropTable("dbo.Brands");
        }
    }
}
