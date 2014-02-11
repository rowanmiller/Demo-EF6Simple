namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNaming : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Blogs", name: "Key", newName: "blog_key");
            RenameColumn(table: "dbo.Blogs", name: "Name", newName: "blog_name");
            RenameColumn(table: "dbo.Blogs", name: "Url", newName: "blog_url");
            RenameColumn(table: "dbo.Blogs", name: "Rating", newName: "blog_rating");
            RenameColumn(table: "dbo.Posts", name: "Key", newName: "post_key");
            RenameColumn(table: "dbo.Posts", name: "Title", newName: "post_title");
            RenameColumn(table: "dbo.Posts", name: "Content", newName: "post_content");
            RenameColumn(table: "dbo.Posts", name: "BlogKey", newName: "post_blog_key");
            RenameColumn(table: "dbo.Comments", name: "Key", newName: "comment_key");
            RenameColumn(table: "dbo.Comments", name: "Author", newName: "comment_author");
            RenameColumn(table: "dbo.Comments", name: "AuthorUrl", newName: "comment_author_url");
            RenameColumn(table: "dbo.Comments", name: "Content", newName: "comment_content");
            RenameColumn(table: "dbo.Comments", name: "PostKey", newName: "comment_post_key");
            RenameColumn(table: "dbo.Comments", name: "StatusKey", newName: "comment_status_key");
            RenameColumn(table: "dbo.Status", name: "Key", newName: "status_key");
            RenameColumn(table: "dbo.Status", name: "Name", newName: "status_name");
            RenameColumn(table: "dbo.Status", name: "DisplayToPublic", newName: "status_display_to_public");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Status", name: "status_display_to_public", newName: "DisplayToPublic");
            RenameColumn(table: "dbo.Status", name: "status_name", newName: "Name");
            RenameColumn(table: "dbo.Status", name: "status_key", newName: "Key");
            RenameColumn(table: "dbo.Comments", name: "comment_status_key", newName: "StatusKey");
            RenameColumn(table: "dbo.Comments", name: "comment_post_key", newName: "PostKey");
            RenameColumn(table: "dbo.Comments", name: "comment_content", newName: "Content");
            RenameColumn(table: "dbo.Comments", name: "comment_author_url", newName: "AuthorUrl");
            RenameColumn(table: "dbo.Comments", name: "comment_author", newName: "Author");
            RenameColumn(table: "dbo.Comments", name: "comment_key", newName: "Key");
            RenameColumn(table: "dbo.Posts", name: "post_blog_key", newName: "BlogKey");
            RenameColumn(table: "dbo.Posts", name: "post_content", newName: "Content");
            RenameColumn(table: "dbo.Posts", name: "post_title", newName: "Title");
            RenameColumn(table: "dbo.Posts", name: "post_key", newName: "Key");
            RenameColumn(table: "dbo.Blogs", name: "blog_rating", newName: "Rating");
            RenameColumn(table: "dbo.Blogs", name: "blog_url", newName: "Url");
            RenameColumn(table: "dbo.Blogs", name: "blog_name", newName: "Name");
            RenameColumn(table: "dbo.Blogs", name: "blog_key", newName: "Key");
        }
    }
}
