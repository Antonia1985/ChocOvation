namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        DateOfOrder = c.DateTime(nullable: false),
                        OfferID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Offer", t => t.OfferID, cascadeDelete: true)
                .Index(t => t.OfferID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "OfferID", "dbo.Offer");
            DropIndex("dbo.Order", new[] { "OfferID" });
            DropTable("dbo.Order");
        }
    }
}
