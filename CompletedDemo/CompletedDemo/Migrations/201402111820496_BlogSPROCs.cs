namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogSPROCs : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Blog_Insert",
                p => new
                    {
                        blog_name = p.String(maxLength: 200),
                        blog_url = p.String(maxLength: 200),
                        blog_rating = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Blogs]([blog_name], [blog_url], [blog_rating])
                      VALUES (@blog_name, @blog_url, @blog_rating)
                      
                      DECLARE @blog_key int
                      SELECT @blog_key = [blog_key]
                      FROM [dbo].[Blogs]
                      WHERE @@ROWCOUNT > 0 AND [blog_key] = scope_identity()
                      
                      SELECT t0.[blog_key]
                      FROM [dbo].[Blogs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[blog_key] = @blog_key"
            );
            
            CreateStoredProcedure(
                "dbo.Blog_Update",
                p => new
                    {
                        blog_key = p.Int(),
                        blog_name = p.String(maxLength: 200),
                        blog_url = p.String(maxLength: 200),
                        blog_rating = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Blogs]
                      SET [blog_name] = @blog_name, [blog_url] = @blog_url, [blog_rating] = @blog_rating
                      WHERE ([blog_key] = @blog_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Blog_Delete",
                p => new
                    {
                        blog_key = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Blogs]
                      WHERE ([blog_key] = @blog_key)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Blog_Delete");
            DropStoredProcedure("dbo.Blog_Update");
            DropStoredProcedure("dbo.Blog_Insert");
        }
    }
}
