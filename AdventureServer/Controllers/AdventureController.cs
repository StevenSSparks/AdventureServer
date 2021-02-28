using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Models;
using AdventureServer.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace AdventureServer.Controllers
{
   
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AdventureController : Controller
    {
        private readonly IPlayAdventure _playAdventure;


        public AdventureController(IPlayAdventure playAdventure)
        {
            _playAdventure = playAdventure;
        }

        // List of Games in the Game Data Store
        [EnableCors("DefaulPolicy")]
        [Route("/api/Adventure/list")]
        [HttpGet]
        public List<Game> GameList() => _playAdventure.ControllerEntry_GetGames();


        // Play Game based on the list of games 
        [EnableCors("DefaulPolicy")]
        [Route("/api/Adventure/{id}")]
        [HttpGet] // returns the game requested
        public GameMoveResult NewGameByID([FromRoute] int id) => _playAdventure.ControllerEntry_NewGame(id);

        // Make a Game Move 
        [EnableCors("DefaulPolicy")]
        [Route("/api/Adventure")]
        [HttpPost]
        public GameMoveResult GameMove(GameMove gm) => _playAdventure.ControllerEntry_GameMove(gm);
   
    }
}
