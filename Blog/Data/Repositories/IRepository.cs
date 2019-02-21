using System;
using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPost();
        void RemovePost(int id);
        void UpdatePost(Post post);
        void AddPost(Post post);

        Task<bool> SaveChangesAsync();

    }
}
