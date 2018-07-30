namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeemployeeuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "EmployeeUser_Id", "dbo.EmployeeUsers");
            DropForeignKey("dbo.AspNetUserLogins", "EmployeeUser_Id", "dbo.EmployeeUsers");
            DropForeignKey("dbo.AspNetUserRoles", "EmployeeUser_Id", "dbo.EmployeeUsers");
            DropIndex("dbo.AspNetUserClaims", new[] { "EmployeeUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "EmployeeUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "EmployeeUser_Id" });
            DropColumn("dbo.AspNetUserClaims", "EmployeeUser_Id");
            DropColumn("dbo.AspNetUserLogins", "EmployeeUser_Id");
            DropColumn("dbo.AspNetUserRoles", "EmployeeUser_Id");
            DropTable("dbo.EmployeeUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ZipCode = c.Int(nullable: false),
                        UserRole = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUserRoles", "EmployeeUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "EmployeeUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "EmployeeUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUserRoles", "EmployeeUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "EmployeeUser_Id");
            CreateIndex("dbo.AspNetUserClaims", "EmployeeUser_Id");
            AddForeignKey("dbo.AspNetUserRoles", "EmployeeUser_Id", "dbo.EmployeeUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "EmployeeUser_Id", "dbo.EmployeeUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "EmployeeUser_Id", "dbo.EmployeeUsers", "Id");
        }
    }
}
