using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AdventureServer.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class PlayAdventureController : Controller
    {

        [Route("/playadventure")]
        [HttpGet]
        public IActionResult Welcome()
        {
            HttpContext.Session.SetString("InstanceID", "-1");
            ViewBag.Message = "";

            return View("index");

        }

    }
}
