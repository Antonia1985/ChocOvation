namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chocoStore2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChocoStore",
                c => new
                    {
                        ChocoStoreID = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                        Address = c.String(),
                        DepartmentID = c.Int(nullable: false),
                        NumberofProductsSoldToday = c.Int(nullable: false),
                        TodaysStock = c.Int(nullable: false),
                        TodaysProfit = c.Int(nullable: false),
                        ManagersID = c.String(name: "Manager's ID", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ChocoStoreID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagersID, cascadeDelete: true)
                .Index(t => t.ManagersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChocoStore", "Manager's ID", "dbo.AspNetUsers");
            DropIndex("dbo.ChocoStore", new[] { "Manager's ID" });
            DropTable("dbo.ChocoStore");
        }
    }
}
