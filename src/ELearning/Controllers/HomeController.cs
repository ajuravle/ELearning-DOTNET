﻿using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {

            return View();
        }

        public IActionResult Learn()
        {

            return View();
        }

        public IActionResult TryIt()
        {

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}