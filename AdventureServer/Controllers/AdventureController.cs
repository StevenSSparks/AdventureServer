using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Models;
using AdventureServer.Interfaces;

namespace AdventureServer.Controllers
{
    [ApiController]
    public class AdventureController : ControllerBase
    {
        private readonly IPlayAdventure _playAdventure;

        public AdventureController(IPlayAdventure playAdventure) => _playAdventure = playAdventure;

        // List of Games in the Game Data Store
        [HttpGet("/api/Adventure/list")]
        public List<Game> GameList() => _playAdventure.ControllerEntry_GetGames();

        // Play First Game 
        [HttpGet("/api/Adventure")] 
        public GameMoveResult NewGame() => _playAdventure.ControllerEntry_NewGame(1);

        // Play Game based on the list of games 
        [HttpGet("/api/Adventure/{id}")] // returns the game requested
        public GameMoveResult NewGameByID([FromRoute] int id) => _playAdventure.ControllerEntry_NewGame(id);

        // Make a Game Move 
        [HttpPost("/api/Adventure")]
        public GameMoveResult GameMove(GameMove gm) => _playAdventure.ControllerEntry_GameMove(gm);
   
    }
}
