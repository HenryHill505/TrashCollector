namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZipAssigment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ZipAssigment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ZipAssigment");
        }
    }
}
