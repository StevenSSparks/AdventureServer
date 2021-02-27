using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Services;
using AdventureServer.Models;
using AdventureServer.Interfaces;

namespace AdventureServer.Controllers
{
    public class WelcomeController : Controller
    {

        private readonly IAppVersionService _appVersionService;

        public WelcomeController(IAppVersionService appVersionService)
        {
            _appVersionService = appVersionService;
        }

        [Route("/")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            var v = new Models.WelcomeVM { AppVersion = _appVersionService.Version };

            // Returns a Static HTML Page
            return View(v);
        }




    }
}
