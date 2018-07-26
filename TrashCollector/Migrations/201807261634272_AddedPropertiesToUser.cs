namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StreetAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AddColumn("dbo.AspNetUsers", "PickupDay", c => c.String());
            AddColumn("dbo.AspNetUsers", "PickupRate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PickupRate");
            DropColumn("dbo.AspNetUsers", "PickupDay");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "ZipCode");
            DropColumn("dbo.AspNetUsers", "StreetAddress");
        }
    }
}
