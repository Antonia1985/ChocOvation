namespace ChocOvation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choco",
                c => new
                    {
                        ChocoID = c.Int(nullable: false, identity: true),
                        ChocoName = c.String(),
                    })
                .PrimaryKey(t => t.ChocoID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                        ManagersID = c.String(name: "Manager's ID", maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagersID)
                .Index(t => t.ManagersID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        VATNumber = c.String(maxLength: 9),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        HireDate = c.DateTime(),
                        Salary = c.Decimal(storeType: "money"),
                        DepartmentID = c.Int(),
                        Profession = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentID)
                .Index(t => t.VATNumber, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.DosePerMaterial",
                c => new
                    {
                        DosePerMaterialID = c.Int(nullable: false, identity: true),
                        ChocoID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        QuantityPer100gr = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DosePerMaterialID)
                .ForeignKey("dbo.Choco", t => t.ChocoID, cascadeDelete: true)
                .ForeignKey("dbo.Material", t => t.MaterialID, cascadeDelete: true)
                .Index(t => t.ChocoID)
                .Index(t => t.MaterialID);
            
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        MaterialID = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Quality = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialID);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        OfferID = c.Int(nullable: false, identity: true),
                        SupplierID = c.String(nullable: false, maxLength: 128),
                        DateOfOffer = c.DateTime(nullable: false),
                        TotalPriceQuality = c.Single(),
                    })
                .PrimaryKey(t => t.OfferID)
                .ForeignKey("dbo.AspNetUsers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.OfferPerMaterial",
                c => new
                    {
                        OfferID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        PricePerKg = c.Int(nullable: false),
                        PriceQuality = c.Single(),
                    })
                .PrimaryKey(t => new { t.OfferID, t.MaterialID })
                .ForeignKey("dbo.Material", t => t.MaterialID, cascadeDelete: true)
                .ForeignKey("dbo.Offer", t => t.OfferID, cascadeDelete: true)
                .Index(t => t.OfferID)
                .Index(t => t.MaterialID);
            
            CreateTable(
                "dbo.Production",
                c => new
                    {
                        ProductionID = c.Int(nullable: false, identity: true),
                        DayOfProduction = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ItemsProducedPerDay = c.Int(nullable: false),
                        FixedCosts = c.Int(nullable: false),
                        ManagersID = c.String(name: "Manager's ID", nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductionID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagersID, cascadeDelete: true)
                .Index(t => t.ManagersID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        BarCode = c.String(nullable: false),
                        DayOfProduction = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        WeightPerItem = c.Int(nullable: false),
                        PricePerItem = c.Int(nullable: false),
                        ChocoID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        ProductionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Choco", t => t.ChocoID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Production", t => t.ProductionID, cascadeDelete: true)
                .Index(t => t.ChocoID)
                .Index(t => t.DepartmentID)
                .Index(t => t.ProductionID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
                .PrimaryKey(t => t.ManagersID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagersID)
                .Index(t => t.ManagersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Store", "Manager's ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Product", "ProductionID", "dbo.Production");
            DropForeignKey("dbo.Product", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Product", "ChocoID", "dbo.Choco");
            DropForeignKey("dbo.Production", "Manager's ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OfferPerMaterial", "OfferID", "dbo.Offer");
            DropForeignKey("dbo.OfferPerMaterial", "MaterialID", "dbo.Material");
            DropForeignKey("dbo.Offer", "SupplierID", "dbo.AspNetUsers");
            DropForeignKey("dbo.DosePerMaterial", "MaterialID", "dbo.Material");
            DropForeignKey("dbo.DosePerMaterial", "ChocoID", "dbo.Choco");
            DropForeignKey("dbo.Department", "Manager's ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Store", new[] { "Manager's ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Product", new[] { "ProductionID" });
            DropIndex("dbo.Product", new[] { "DepartmentID" });
            DropIndex("dbo.Product", new[] { "ChocoID" });
            DropIndex("dbo.Production", new[] { "Manager's ID" });
            DropIndex("dbo.OfferPerMaterial", new[] { "MaterialID" });
            DropIndex("dbo.OfferPerMaterial", new[] { "OfferID" });
            DropIndex("dbo.Offer", new[] { "SupplierID" });
            DropIndex("dbo.DosePerMaterial", new[] { "MaterialID" });
            DropIndex("dbo.DosePerMaterial", new[] { "ChocoID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "VATNumber" });
            DropIndex("dbo.Department", new[] { "Manager's ID" });
            DropTable("dbo.Store");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Product");
            DropTable("dbo.Production");
            DropTable("dbo.OfferPerMaterial");
            DropTable("dbo.Offer");
            DropTable("dbo.Material");
            DropTable("dbo.DosePerMaterial");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Department");
            DropTable("dbo.Choco");
        }
    }
}
