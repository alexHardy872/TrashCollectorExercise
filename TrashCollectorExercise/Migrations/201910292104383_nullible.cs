namespace TrashCollectorExercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullible : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "startBreak", c => c.DateTime());
            AlterColumn("dbo.Customers", "endBreak", c => c.DateTime());
            AlterColumn("dbo.Customers", "oneTimePickup", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "oneTimePickup", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "endBreak", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "startBreak", c => c.DateTime(nullable: false));
        }
    }
}
