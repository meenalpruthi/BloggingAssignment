namespace Blogging_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyBloggingAssignmentDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Posts", t => t.post_Id)
                .Index(t => t.post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Name = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Views = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Published = c.Boolean(nullable: false),
                        Created_At = c.DateTime(nullable: false),
                        Updated_At = c.DateTime(nullable: false),
                        Number_Of_Comments = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "post_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "post_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
