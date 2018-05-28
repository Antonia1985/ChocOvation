namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Store", "Manager's ID", "dbo.AspNetUsers");
            DropIndex("dbo.Store", new[] { "Manager's ID" });
            DropTable("dbo.Store");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        ManagersID = c.String(name: "Manager's ID", nullable: false, maxLength: 128),
                        StoreID = c.Int(nullable: false),
                        StoreName = c.String(),
                        Address = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagersID);
            
            CreateIndex("dbo.Store", "Manager's ID");
            AddForeignKey("dbo.Store", "Manager's ID", "dbo.AspNetUsers", "Id");
        }
    }
}
