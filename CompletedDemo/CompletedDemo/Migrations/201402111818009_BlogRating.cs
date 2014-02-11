namespace CompletedDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Rating", c => c.Int(nullable: false, defaultValue: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Rating");
        }
    }
}
