using System;
namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Author { get; set; } = "John M";


    }
}
