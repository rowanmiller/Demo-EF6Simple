namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllSPROCs : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Post_Insert",
                p => new
                    {
                        post_title = p.String(maxLength: 200),
                        post_content = p.String(maxLength: 200),
                        post_blog_key = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Posts]([post_title], [post_content], [post_blog_key])
                      VALUES (@post_title, @post_content, @post_blog_key)
                      
                      DECLARE @post_key int
                      SELECT @post_key = [post_key]
                      FROM [dbo].[Posts]
                      WHERE @@ROWCOUNT > 0 AND [post_key] = scope_identity()
                      
                      SELECT t0.[post_key]
                      FROM [dbo].[Posts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[post_key] = @post_key"
            );
            
            CreateStoredProcedure(
                "dbo.Post_Update",
                p => new
                    {
                        post_key = p.Int(),
                        post_title = p.String(maxLength: 200),
                        post_content = p.String(maxLength: 200),
                        post_blog_key = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Posts]
                      SET [post_title] = @post_title, [post_content] = @post_content, [post_blog_key] = @post_blog_key
                      WHERE ([post_key] = @post_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Post_Delete",
                p => new
                    {
                        post_key = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Posts]
                      WHERE ([post_key] = @post_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Comment_Insert",
                p => new
                    {
                        comment_author = p.String(maxLength: 200),
                        comment_author_url = p.String(maxLength: 200),
                        comment_content = p.String(maxLength: 200),
                        comment_post_key = p.Int(),
                        comment_status_key = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Comments]([comment_author], [comment_author_url], [comment_content], [comment_post_key], [comment_status_key])
                      VALUES (@comment_author, @comment_author_url, @comment_content, @comment_post_key, @comment_status_key)
                      
                      DECLARE @comment_key int
                      SELECT @comment_key = [comment_key]
                      FROM [dbo].[Comments]
                      WHERE @@ROWCOUNT > 0 AND [comment_key] = scope_identity()
                      
                      SELECT t0.[comment_key]
                      FROM [dbo].[Comments] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[comment_key] = @comment_key"
            );
            
            CreateStoredProcedure(
                "dbo.Comment_Update",
                p => new
                    {
                        comment_key = p.Int(),
                        comment_author = p.String(maxLength: 200),
                        comment_author_url = p.String(maxLength: 200),
                        comment_content = p.String(maxLength: 200),
                        comment_post_key = p.Int(),
                        comment_status_key = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Comments]
                      SET [comment_author] = @comment_author, [comment_author_url] = @comment_author_url, [comment_content] = @comment_content, [comment_post_key] = @comment_post_key, [comment_status_key] = @comment_status_key
                      WHERE ([comment_key] = @comment_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Comment_Delete",
                p => new
                    {
                        comment_key = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Comments]
                      WHERE ([comment_key] = @comment_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Insert",
                p => new
                    {
                        status_name = p.String(maxLength: 200),
                        status_display_to_public = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Status]([status_name], [status_display_to_public])
                      VALUES (@status_name, @status_display_to_public)
                      
                      DECLARE @status_key int
                      SELECT @status_key = [status_key]
                      FROM [dbo].[Status]
                      WHERE @@ROWCOUNT > 0 AND [status_key] = scope_identity()
                      
                      SELECT t0.[status_key]
                      FROM [dbo].[Status] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[status_key] = @status_key"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Update",
                p => new
                    {
                        status_key = p.Int(),
                        status_name = p.String(maxLength: 200),
                        status_display_to_public = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Status]
                      SET [status_name] = @status_name, [status_display_to_public] = @status_display_to_public
                      WHERE ([status_key] = @status_key)"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Delete",
                p => new
                    {
                        status_key = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Status]
                      WHERE ([status_key] = @status_key)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Status_Delete");
            DropStoredProcedure("dbo.Status_Update");
            DropStoredProcedure("dbo.Status_Insert");
            DropStoredProcedure("dbo.Comment_Delete");
            DropStoredProcedure("dbo.Comment_Update");
            DropStoredProcedure("dbo.Comment_Insert");
            DropStoredProcedure("dbo.Post_Delete");
            DropStoredProcedure("dbo.Post_Update");
            DropStoredProcedure("dbo.Post_Insert");
        }
    }
}
