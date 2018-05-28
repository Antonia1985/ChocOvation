namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chocoStoreDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChocoStore", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChocoStore", "Date");
        }
    }
}
