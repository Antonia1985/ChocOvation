namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Production", "DayOfProduction", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Product", "DayOfProduction", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "DayOfProduction", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Production", "DayOfProduction", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
