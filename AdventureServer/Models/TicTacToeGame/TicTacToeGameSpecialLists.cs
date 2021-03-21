using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models.TicTacToeGame
{
    public class TicTacToeGameSpecialLists
    {
        public int EmptySpots { get; set; }
        public List<int> FirstMoves { get; set; }
        public List<int> BoardValues { get; set; }
    }
}
