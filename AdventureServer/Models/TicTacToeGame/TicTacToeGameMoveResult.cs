using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models.TicTacToeGame
{
    public class TicTacToeMoveResult
    {
        public string InstanceID { get; set; }
        public string GameBoard { get; set; }
        public string GameState { get; set; }
        public string GameMessage { get; set; }
        public bool GameOver { get; set; }
    }
}
