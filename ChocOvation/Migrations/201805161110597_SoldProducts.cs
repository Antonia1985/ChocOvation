namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoldProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SoldProduct",
                c => new
                    {
                        SoldProductID = c.Int(nullable: false, identity: true),
                        DateSold = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SoldProductID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SoldProduct", "ProductID", "dbo.Product");
            DropIndex("dbo.SoldProduct", new[] { "ProductID" });
            DropTable("dbo.SoldProduct");
        }
    }
}
