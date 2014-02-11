namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TweakSPROC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCreationLog",
                c => new
                {
                    blog_key = c.Int(nullable: false),
                    created_by = c.String(),
                    created_date = c.DateTime(nullable: false)
                })
                .PrimaryKey(t => t.blog_key);

            RenameStoredProcedure(name: "dbo.Blog_Insert", newName: "CreateBlog");
            AlterStoredProcedure(
                "dbo.CreateBlog",
                p => new
                    {
                        blog_name = p.String(maxLength: 200),
                        blog_url = p.String(maxLength: 200),
                        blog_score = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Blogs]([blog_name], [blog_url], [blog_rating])
                      VALUES (@blog_name, @blog_url, @blog_score)
                      
                      DECLARE @blog_key int
                      SELECT @blog_key = [blog_key]
                      FROM [dbo].[Blogs]
                      WHERE @@ROWCOUNT > 0 AND [blog_key] = scope_identity()

                      INSERT INTO [dbo].[BlogCreationLog] (blog_key, created_by, created_date) 
                      VALUES (@blog_key, SYSTEM_USER, GETDATE())
                      
                      SELECT t0.[blog_key]
                      FROM [dbo].[Blogs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[blog_key] = @blog_key"
            );
            
        }
        
        public override void Down()
        {
            RenameStoredProcedure(name: "dbo.CreateBlog", newName: "Blog_Insert");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
