using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);

        }

        public List<Post> GetAllPost()
        {
            return _ctx.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {

            if (await _ctx.SaveChangesAsync() > 0) {
                return true;
              }
            return false;
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
    }
}
