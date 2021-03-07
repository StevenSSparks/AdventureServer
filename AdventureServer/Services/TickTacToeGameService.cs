using AdventureServer.Interfaces;
using AdventureServer.Models.TicTacToeGame;


namespace AdventureServer.Services.TicTacToeGameService
{
    public class TickTacToeGameService : IPlayTicTacToe
    {

        // Game Caching Service
        public IGameCache _gameCache;

        public TickTacToeGameService(IGameCache gameCache)
        {
            _gameCache = gameCache;
        }

        public TicTacToeMoveResult ControllerEntry_NewGame()
        {


            return new TicTacToeMoveResult();
        }

        public TicTacToeMoveResult ControllerEntry_GameMove(TicTacToeGameMove gameMove)
        {


            return new TicTacToeMoveResult();
        }


    }
}
