namespace TrashCollectorExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        streetAdress = c.String(nullable: false),
                        city = c.String(nullable: false),
                        state = c.String(nullable: false),
                        zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        pickupDay = c.Int(nullable: false),
                        balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        startBreak = c.DateTime(nullable: false),
                        endBreak = c.DateTime(nullable: false),
                        oneTimePickup = c.DateTime(nullable: false),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.AddressId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        zipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "ApplicationId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
