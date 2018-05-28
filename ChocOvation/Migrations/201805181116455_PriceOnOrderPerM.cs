namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceOnOrderPerM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderPerMaterial", "PricePerMaterial", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderPerMaterial", "PricePerMaterial");
        }
    }
}
