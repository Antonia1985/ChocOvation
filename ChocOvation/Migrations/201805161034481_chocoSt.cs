namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chocoSt : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ChocoStore", "DepartmentID");
            AddForeignKey("dbo.ChocoStore", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChocoStore", "DepartmentID", "dbo.Department");
            DropIndex("dbo.ChocoStore", new[] { "DepartmentID" });
        }
    }
}
