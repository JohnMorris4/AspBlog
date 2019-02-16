using System;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
namespace Blog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
       public DbSet<Post> Posts { set; get; }
    }
}
