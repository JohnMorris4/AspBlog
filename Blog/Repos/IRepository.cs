using System;
using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Repos
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPost(int id);
        void RemovePost(int id);
        void UpdatePost(Post post);
        void AddPost(Post post);

        Task<bool> SaveChangesAsync();

    }
}
