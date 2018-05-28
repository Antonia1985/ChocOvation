namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderPerMat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderPerMaterial",
                c => new
                    {
                        OrderPerMaterialID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        QuantityPerYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderPerMaterialID)
                .ForeignKey("dbo.Material", t => t.MaterialID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MaterialID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPerMaterial", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderPerMaterial", "MaterialID", "dbo.Material");
            DropIndex("dbo.OrderPerMaterial", new[] { "MaterialID" });
            DropIndex("dbo.OrderPerMaterial", new[] { "OrderID" });
            DropTable("dbo.OrderPerMaterial");
        }
    }
}
