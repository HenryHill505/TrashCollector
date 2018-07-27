namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTypoinPickupModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "EmployeeId", c => c.String());
            DropColumn("dbo.Pickups", "EployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "EployeeId", c => c.String());
            DropColumn("dbo.Pickups", "EmployeeId");
        }
    }
}
