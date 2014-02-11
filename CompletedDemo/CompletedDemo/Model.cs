using System.Collections.Generic;

namespace CompletedDemo
{
    public class Blog
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Key { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogKey { get; set; }
        public virtual Blog Blog { get; set; }
        public List<Comment> Comments { get; set; }

    }

    public class Comment
    {
        public int Key { get; set; }
        public string Author { get; set; }
        public string AuthorUrl { get; set; }
        public string Content { get; set; }

        public int PostKey { get; set; }
        public Post Post { get; set; }

        public int StatusKey { get; set; }
        public Status Status { get; set; }
    }

    public class Status
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public bool DisplayToPublic { get; set; }
    }
}
