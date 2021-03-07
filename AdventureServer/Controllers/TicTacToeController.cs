using Microsoft.AspNetCore.Mvc;
using AdventureServer.Interfaces;
using Microsoft.AspNetCore.Cors;
using AdventureServer.Models.TicTacToeGame;

namespace AdventureServer.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    public class TicTacToeController : Controller
    {

        private readonly IPlayTicTacToe _playtictactoe;

        public TicTacToeController(IPlayTicTacToe playTicTacToe)
        {
            _playtictactoe = playTicTacToe;
        }


        [EnableCors("CORSPolicy")]
        [Route("/api/TicTacToe/")]
        [HttpGet] // Gets a new game 
        public TicTacToeMoveResult NewGame() => _playtictactoe.ControllerEntry_NewGame();

        // Make a Game Move 
        [EnableCors("CORSPolicy")]
        [Route("/api/TicTacToe")]
        [HttpPost]
        public TicTacToeMoveResult GameMove(TicTacToeGameMove gm) => _playtictactoe.ControllerEntry_GameMove(gm);
        

    }
}
