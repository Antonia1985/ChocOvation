namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateAtSoldProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SoldProduct", "DateSold", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SoldProduct", "DateSold", c => c.DateTime(nullable: false));
        }
    }
}
