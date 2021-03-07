using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models.TicTacToeGame
{
    public class TicTacToeGame
    {
        public string XPlayerName { get; set; }
        public bool XPlayerComputer { get; set; }
        public string OPlayerName { get; set; }
        public bool OPlayerComputer { get; set; }
        public List<string> GameBoard { get; set; }
        public string GameWinner { get; set; }
        public bool GameOver { get; set; }

    }

}
