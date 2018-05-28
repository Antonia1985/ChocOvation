namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSoldToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsSold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsSold");
        }
    }
}
