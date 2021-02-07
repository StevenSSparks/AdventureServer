using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Models;

namespace AdventureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureController : ControllerBase
    {
        [HttpGet("/api/Adventure")] 
        public GameMoveResult NewGame()
        {
            return new GameMoveResult();
        }



    }
}
