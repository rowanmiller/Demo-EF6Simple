namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        BlogKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Blogs", t => t.BlogKey, cascadeDelete: true)
                .Index(t => t.BlogKey);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        AuthorUrl = c.String(),
                        Content = c.String(),
                        PostKey = c.Int(nullable: false),
                        StatusKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Posts", t => t.PostKey, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusKey, cascadeDelete: true)
                .Index(t => t.PostKey)
                .Index(t => t.StatusKey);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayToPublic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "StatusKey", "dbo.Status");
            DropForeignKey("dbo.Comments", "PostKey", "dbo.Posts");
            DropForeignKey("dbo.Posts", "BlogKey", "dbo.Blogs");
            DropIndex("dbo.Comments", new[] { "StatusKey" });
            DropIndex("dbo.Comments", new[] { "PostKey" });
            DropIndex("dbo.Posts", new[] { "BlogKey" });
            DropTable("dbo.Status");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Blogs");
        }
    }
}
