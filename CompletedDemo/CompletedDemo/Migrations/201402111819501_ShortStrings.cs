namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortStrings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "blog_name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Blogs", "blog_url", c => c.String(maxLength: 200));
            AlterColumn("dbo.Posts", "post_title", c => c.String(maxLength: 200));
            AlterColumn("dbo.Posts", "post_content", c => c.String(maxLength: 200));
            AlterColumn("dbo.Comments", "comment_author", c => c.String(maxLength: 200));
            AlterColumn("dbo.Comments", "comment_author_url", c => c.String(maxLength: 200));
            AlterColumn("dbo.Comments", "comment_content", c => c.String(maxLength: 200));
            AlterColumn("dbo.Status", "status_name", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Status", "status_name", c => c.String());
            AlterColumn("dbo.Comments", "comment_content", c => c.String());
            AlterColumn("dbo.Comments", "comment_author_url", c => c.String());
            AlterColumn("dbo.Comments", "comment_author", c => c.String());
            AlterColumn("dbo.Posts", "post_content", c => c.String());
            AlterColumn("dbo.Posts", "post_title", c => c.String());
            AlterColumn("dbo.Blogs", "blog_url", c => c.String());
            AlterColumn("dbo.Blogs", "blog_name", c => c.String());
        }
    }
}
