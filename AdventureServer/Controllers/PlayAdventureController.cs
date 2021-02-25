using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureServer.Models;
using AdventureServer.AdventureData;


namespace AdventureServer.Controllers
{

    // [ApiExplorerSettings(IgnoreApi = true)]
    public class PlayAdventureController : Controller
    {
        private readonly AdventureController _adventureController;

    public PlayAdventureController (AdventureController adventureController)
        {
            _adventureController = adventureController;
        }


        [Route("/playadventure")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Welcome()
        {
            HttpContext.Session.SetString("InstanceID", "-1");
            ViewBag.Message = "";

            return View("index");

        }

        // This is simple client to play the adventure game
        // This defaults to Adventure House - Game 1
        // TODO: Change this endpoint to Adventure House and let people pick the game before we get here
        // The command is posted as well as the display buffer so that each time you do a command it looks
        // like the screen is scrolling up.
        [Route("/PlayAdventure")]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult PlayGame(string command, string buffer)
        {
            var _playgame = new PlayGameVM();

            //Check to see if the player has an active game
            string _InstanceID = HttpContext.Session.GetString("InstanceID");

            if (_InstanceID == "-1") // If no active game then crete a new game
            {

                var gameMove = _adventureController.NewGame();
                HttpContext.Session.SetString("InstanceID", gameMove.InstanceID.ToString());
                _playgame.Buffer = "";
                _playgame.RoomName = gameMove.RoomName;
                _playgame.Message = gameMove.RoomMessage;
                _playgame.Items = gameMove.ItemsMessage;
                _playgame.HealthReport = "Good To Go!";
                _playgame.GamerTag = gameMove.PlayerName;

                return View("playgame", _playgame);
            }

            // clean up the command 
            if (command == null)
            {
                command = "";
            }
            else
            {
                var clen = command.Length;
                if (clen > 64) clen = 64;
                command = command.Substring(0, clen); // limit the input length
            }

            var move = new GameMove
            {
                Move = command,
                InstanceID = _InstanceID
            };

            var gmr = _adventureController.GameMove(move);

            // Set the Instance ID in the session    
            HttpContext.Session.SetString("InstanceID", gmr.InstanceID.ToString());

            // map the game move result to the output view model
            _playgame.RoomName = gmr.RoomName;
            _playgame.Message = gmr.RoomMessage;
            _playgame.Items = gmr.ItemsMessage;
            _playgame.HealthReport = gmr.HealthReport;
            _playgame.GamerTag = gmr.PlayerName;

            // add the command to the buffer. The greater than below is the command prompt
            string newbuffer;
            newbuffer = buffer + "\r\n>" + command + "\r\n";
            _playgame.Buffer = newbuffer;

            return View("playgame", _playgame);
        }
    }

}

