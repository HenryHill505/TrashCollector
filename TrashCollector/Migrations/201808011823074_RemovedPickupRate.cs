namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPickupRate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "PickupRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "PickupRate", c => c.String());
        }
    }
}
