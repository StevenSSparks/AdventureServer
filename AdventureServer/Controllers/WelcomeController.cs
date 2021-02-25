using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureServer.Controllers
{
    // [ApiExplorerSettings(IgnoreApi = true)]
    public class WelcomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            int numTimes = 0;

            ViewBag.Title = "Adventure Server 1.0";
            ViewData["Message"] = "";
            ViewData["NumTimes"] = numTimes;

            return View();
        }

    }
}
