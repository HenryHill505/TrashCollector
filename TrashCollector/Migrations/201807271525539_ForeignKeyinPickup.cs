namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyinPickup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pickups", "UserId");
            AddForeignKey("dbo.Pickups", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Pickups", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Pickups", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Pickups", new[] { "UserId" });
            DropColumn("dbo.Pickups", "UserId");
        }
    }
}
