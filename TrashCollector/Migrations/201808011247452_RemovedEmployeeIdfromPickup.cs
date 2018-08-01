namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEmployeeIdfromPickup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pickups", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "EmployeeId", c => c.String());
        }
    }
}
