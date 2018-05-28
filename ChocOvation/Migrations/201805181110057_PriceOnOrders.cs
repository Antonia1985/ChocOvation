namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceOnOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Order", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Order", "Price");
        }
    }
}
