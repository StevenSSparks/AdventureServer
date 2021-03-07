using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models.AdventureGame;

namespace AdventureServer.Interfaces
{
    public interface IPlayTextAdventure 
    {
        public GameMoveResult ControllerEntry_NewGame(int GameID);

        public List<Game> ControllerEntry_GetGames();

        public GameMoveResult ControllerEntry_GameMove(GameMove gameMove);

    }
}
