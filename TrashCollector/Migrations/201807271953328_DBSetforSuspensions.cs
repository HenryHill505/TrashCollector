namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBSetforSuspensions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suspensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suspensions", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Suspensions", new[] { "UserID" });
            DropTable("dbo.Suspensions");
        }
    }
}
