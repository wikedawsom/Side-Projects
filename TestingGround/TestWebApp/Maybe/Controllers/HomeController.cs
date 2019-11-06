using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Test Homepage
        /// </summary>
        /// <returns></returns>
        public IActionResult HomePage()
        {
            return View(new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5
            });
        }
        /// <summary>
        /// Theoretical Privacy Policy Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
        
        /// <summary>
        /// ASP.NET Default Homepage
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}