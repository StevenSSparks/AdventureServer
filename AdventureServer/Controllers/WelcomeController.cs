using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureServer.Controllers
{
    public class WelcomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            // Returns a Static HTML Page
            return View();
        }

    }
}
