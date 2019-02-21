using System;
using System.Threading.Tasks;
using Blog.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
using Blog.Data.Repositories;
using Blog.Data.FileManager;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        readonly IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _repo = repo;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPost();
            return View(posts);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags

                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Category = vm.Category,
                Tags = vm.Tags,
                Description = vm.Description

            };
            if(vm.Image == null)
            {
                post.Image = vm.CurrentImage;
            }
            else
            {
                post.Image = await _fileManager.SaveImage(vm.Image);
            }
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }
    }
}
