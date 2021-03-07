using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace AdventureServer.Controllers
{

    public class PlayTicTacToeController : Controller
    {

       //private readonly TicTacToeController _ticTacToeController;

       // public PlayTicTacToeController(TicTacToeController ticTacToeController)
       // {
       //     _ticTacToeController = ticTacToeController;
       // }


        [Route("/playtictactoe")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult PlayTicTacToe()
        {

            //welcome page to start the game
            // post to the endpoint and then loop the game 
            HttpContext.Session.SetString("TicTacToeInstanceID", "-1");
            ViewBag.Message = "";

            return View("index");

        }


        [Route("/playtictactoe")]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult PlayGame(int id, string move, string buffer)
        {
            // If game does not exist then start the game
            //  var newTicTacToeGame = _playtictactoe.NewGame();
            // play the game 

                // code the game play

            return View("playgame");

        }



    }
}
