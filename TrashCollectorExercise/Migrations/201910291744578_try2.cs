namespace TrashCollectorExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class try2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "AddressId" });
            AddColumn("dbo.Customers", "streetAdress", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "city", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "state", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "zip", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "AddressId", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "zip");
            DropColumn("dbo.Customers", "state");
            DropColumn("dbo.Customers", "city");
            DropColumn("dbo.Customers", "streetAdress");
            CreateIndex("dbo.Customers", "AddressId");
            AddForeignKey("dbo.Customers", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
