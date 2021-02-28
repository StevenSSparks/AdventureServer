using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Services;
using AdventureServer.Models;
using AdventureServer.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace AdventureServer.Controllers
{
    public class WelcomeController : Controller
    {

        private readonly IAppVersionService _appVersionService;

        public WelcomeController(IAppVersionService appVersionService)
        {
            _appVersionService = appVersionService;
        }

        [EnableCors("DefaulPolicy")]
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
