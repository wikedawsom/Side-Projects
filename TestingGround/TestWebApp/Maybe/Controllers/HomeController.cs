using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult HomePage()
        {
            object dataModel = new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5
            };
            return View(dataModel);
        }
    }
}