using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;

namespace AdventureServer.Interfaces
{
    public interface IPlayAdventure 
    {
        public GameMoveResult ControllerEntry_NewGame(int GameID);

        public List<Game> ControllerEntry_GetGames();

        GameMoveResult ControllerEntry_GameMove(GameMove gameMove);

    }
}
