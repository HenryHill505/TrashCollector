namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickupType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pickups", "Type");
        }
    }
}
