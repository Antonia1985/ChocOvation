namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datesOnOrdersOffers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offer", "DateOfOffer", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Order", "DateOfOrder", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "DateOfOrder", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Offer", "DateOfOffer", c => c.DateTime(nullable: false));
        }
    }
}
