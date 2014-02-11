using System.Data.Entity;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CompletedDemo
{
    class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Properties()
                .Where(p => p.Name == "Key")
                .Configure(p => p.IsKey());

            modelBuilder
                .Properties()
                .Configure(p => p.HasColumnName(GetColumnName(p.ClrPropertyInfo)));

            modelBuilder
                .Properties<string>()
                .Configure(p => p.HasMaxLength(200));

            modelBuilder
                .Entity<Blog>()
                .MapToStoredProcedures(s =>
                    s.Insert(i => i.HasName("CreateBlog").Parameter(b => b.Rating, "blog_score")));

            modelBuilder
                .Types()
                .Configure(t => t.MapToStoredProcedures());
        }

        /// <summary>
        /// Combines type and property name using underscore naming
        /// e.g.  Comment.Key => comment_key
        ///       Comment.AuthorUrl => comment_author_url
        /// </summary>
        private static string GetColumnName(PropertyInfo property)
        {
            var result = property.DeclaringType.Name + "_";

            result += Regex.Replace(
                property.Name,
                ".[A-Z]",
                m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }
    }
}
