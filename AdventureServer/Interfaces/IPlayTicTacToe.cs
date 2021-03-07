using AdventureServer.Models.TicTacToeGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Interfaces
{
    public interface IPlayTicTacToe
    {

        public TicTacToeMoveResult ControllerEntry_NewGame();

        public TicTacToeMoveResult ControllerEntry_GameMove(TicTacToeGameMove gameMove);

    }
}
