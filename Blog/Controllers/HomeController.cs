﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
