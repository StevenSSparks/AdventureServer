using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;

namespace AdventureServer.Interfaces
{
    public interface IPlayAdventure 
    {
        public GameMoveResult NewGame(int GameID);

        public List<Game> GetGames();

        GameMoveResult GameMove(GameMove gameMove);

    }
}
