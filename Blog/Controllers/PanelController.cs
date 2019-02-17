using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Blog.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
