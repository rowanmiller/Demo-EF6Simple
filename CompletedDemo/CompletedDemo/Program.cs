using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = CreateAndPrintBlogs();

            Console.WriteLine("Quote: " + QuoteOfTheDay.Quotes.GetQuote());

            task.Wait();
        }

        static async Task CreateAndPrintBlogs()
        {
            using (var db = new BloggingContext())
            {
                db.Database.Log = Console.Write;

                db.Blogs.Add(new Blog { Name = "Yet Another Blog #" + (db.Blogs.Count() + 1) });
                await db.SaveChangesAsync();

                var blogs = await (from b in db.Blogs
                                   orderby b.Name
                                   select b).ToListAsync();

                Console.WriteLine("Blogs:");
                foreach (var item in blogs)
                {
                    Console.WriteLine(" - " + item.Name);
                }
            }
        }
    }
}
