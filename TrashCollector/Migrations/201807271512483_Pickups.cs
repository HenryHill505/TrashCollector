namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pickups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        EployeeId = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pickups");
        }
    }
}
