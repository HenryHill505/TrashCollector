namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeZipAssignmentnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "ZipAssigment", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ZipAssigment", c => c.Int(nullable: false));
        }
    }
}
