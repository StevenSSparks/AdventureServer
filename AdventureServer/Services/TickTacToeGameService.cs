using AdventureServer.Interfaces;
using AdventureServer.Models.TicTacToeGame;
using System.Collections.Generic;
using AdventureServer.GameData;
using System;

namespace AdventureServer.Services.TicTacToeGameService
{
    public class TickTacToeGameService : IPlayTicTacToe
    {

        // Game Caching Service
        public IGameCache _gameCache;
        public TicTacToe _ticTacToedata;


        public TickTacToeGameService(IGameCache gameCache, TicTacToe ticTacToeData )
        {
            _gameCache = gameCache;
            _ticTacToedata = ticTacToeData;
        }



        private TicTacToeMoveResult ComputerVComputer()
        {
            return new TicTacToeMoveResult
            {


            };

        }

        public TicTacToeMoveResult ControllerEntry_NewGame()
        {


            return new TicTacToeMoveResult();
        }

        public TicTacToeMoveResult ControllerEntry_GameMove(TicTacToeGameMove gameMove)
        {


            return new TicTacToeMoveResult();
        }


        // Set Player Token

        // GameBoard Spot Full

        // Valid Game Board Position - Is the choice 1 to 9 

        // Valid Game Choice - Can we move here?

      
      


        // Add the player Token to the gameboard string array
        private string[,] GameBoardSetPlayerToken(TicTacToeGame ticTacToeGame, int position, string token)
        {

            switch (position)
            {
                case 0:
                    ticTacToeGame.GameBoardArray[0, 0] = token;
                    break;
                case 1:
                    ticTacToeGame.GameBoardArray[0, 1] = token;
                    break;
                case 2:
                    ticTacToeGame.GameBoardArray[0, 2] = token;
                    break;
                case 3:
                    ticTacToeGame.GameBoardArray[1, 0] = token;
                    break;
                case 4:
                    ticTacToeGame.GameBoardArray[1, 1] = token;
                    break;
                case 5:
                    ticTacToeGame.GameBoardArray[1, 2] = token;
                    break;
                case 6:
                    ticTacToeGame.GameBoardArray[2, 0] = token;
                    break;
                case 7:
                    ticTacToeGame.GameBoardArray[2, 1] = token;
                    break;
                case 8:
                    ticTacToeGame.GameBoardArray[2, 2] = token;
                    break;

            }

            return ticTacToeGame.GameBoardArray;

        }
        // Produces game play message
        private string MoveMsgwBoardHuman(string cm, string pn, string pt, string[,] gb, string extramsg)
        {
            string msg = cm;
            msg = msg + "Your move " + pn + "." + "\r\n";
            msg = msg + "You are playing " + pt + "s." + "\r\n";
            msg = msg + "\r\n" + DrawBoard(gb) + "\r\n";

            if (extramsg != "")
            {
                msg = msg + "------------------------------------" + "\r\n";
                msg = msg + extramsg + "\r\n";
                msg = msg + "------------------------------------" + "\r\n" + "\r\n";

            }

            msg = msg + "Please select a poistion:";
            return msg;
        }
        private string MoveMsgwBoardComputer(string cm, string pn, string pt, string[,] gb, string extramsg)
        {
            string msg = cm;
            msg = msg + "Move by " + pn + "." + "\r\n";
            msg = msg + "Computer is playing " + pt + "s." + "\r\n";

            if (extramsg != "")
            {
                msg = msg + "------------------------------------" + "\r\n";
                msg = msg + extramsg + "\r\n";
                msg = msg + "------------------------------------" + "\r\n" + "\r\n";

            }
            msg = msg + "\r\n" + DrawBoard(gb) + "\r\n";
            return msg;
        }
        public string DrawBoard(string[,] gameboard)
        {
            string board = "";

            board = board + "\r\n" + "\r\n";

            for (int x = 0; x < 3; x++)
            {

                board = board + "  " + gameboard[x, 0] + " | " + gameboard[x, 1] + " | " + gameboard[x, 2] + "\r\n";

                if (x == 0 || x == 1)
                {
                    board = board + " ---+---+--- " + "\r\n";

                };

            }

            board = board + "\r\n";
            board = board + "\r\n";

            return board;
        }


        // Provides a recommended move 
        // This is used to generate the computer move as well as provide message 
        // the the help or cheats commands used. 

        public TicTacToeGameSpecialLists GameBoardLists(string[,] gba)
        {
            var sl = new TicTacToeGameSpecialLists
            {
                FirstMoves = _ticTacToedata.RecommendedFirstMoves,
                BoardValues = _ticTacToedata.BoardValuesList, // array of 8 0s
                EmptySpots = 0
            };

            int bspot = 0;
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    bspot++;

                    var token = gba[x, y];
                    if (gba[x, y] != "X" && gba[x, y] != "Y") { sl.EmptySpots++; }
                    else
                    {
                        if (gba[x, y] == "X") sl.BoardValues[bspot] = 1;
                        if (gba[x, y] == "Y") sl.BoardValues[bspot] = 2;
                    }
                }
            return sl;
        }
                
       

        public RecommendMove RecommendMove(TicTacToeGame game, string player)
        {
            // This is a bit brute force 
            var rm = new RecommendMove();
            var sl = GameBoardLists(game.GameBoardArray);
            
            // If all the spots are empty provide ramdom but good first move
            // corners or center.
            if (sl.EmptySpots == 9)
            {
                Random rnd1 = new Random();
                int mv = rnd1.Next(1, sl.FirstMoves.Count);
                int move = sl.FirstMoves[mv];
                var message = "Board has " + sl.EmptySpots.ToString() + " empty spots.";
                message = message + "\r\n" + "Consider spot " + move.ToString() + " as a first move.";
                rm.message = message;
                rm.move = move;
                return rm;
            }



            return rm;
        }







        // Parsing methods to support finding a move 

        // Creates a list of spaces where you need to play to block a win
        private List<int> FindGameBoardBlocks(List<int> boardValues, int tokenMove, int tokenBlock)
        {
            // before we get here, the assumpton is that the game is not in the state of win. 
            // generally we should only see 1 item or 0 item in the final list, unless we screwed up on a move

            List<int> blockList = new List<int>();

            for (int x = 0; x < 9; x++)
            {

                if (boardValues[x] != 0 && boardValues[x] != tokenMove)
                {
                    if (x == 0)
                    { // corner

                        if ((boardValues[x] == tokenBlock) && (boardValues[1] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[3] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }

                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }


                    }

                    if (x == 1)
                    { // side

                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[7] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                    }

                    if (x == 2)
                    { // corner

                        if ((boardValues[x] == tokenBlock) && (boardValues[1] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[5] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }

                    }

                    if (x == 3)
                    { // side

                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[5] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                    }

                    if (x == 4)
                    { // center

                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[1] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[7] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[3] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[5] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }

                    }

                    if (x == 5)
                    { // side

                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[3] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                    }

                    if (x == 6)
                    { // corner

                        if ((boardValues[x] == tokenBlock) && (boardValues[3] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[7] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                    }

                    if (x == 7)
                    { // side

                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[8] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[1] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                    }

                    if (x == 8)
                    { // corner

                        if ((boardValues[x] == tokenBlock) && (boardValues[5] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[2] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[7] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[6] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[4] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenBlock) && (boardValues[0] == tokenBlock)) { blockList = AddNonDupBlock(blockList, boardValues[4], 4); }
                    }
                }


            }

            return blockList;
        }
        private List<int> FindGameBoardBlocksSpecial(List<int> boardValues, int tokenMove, int tokenBlock)
        {

            // Only use these blocks if the other blocks are count = 0

            List<int> blockList = new List<int>();

            // two corners have same token
            if ((boardValues[0] == tokenBlock) && (boardValues[8] == tokenBlock) && (boardValues[4] == tokenMove))
            {

                if (boardValues[1] == 0)
                { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                else
                {
                    if (boardValues[3] == 0)
                    { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }
                    else
                    {
                        if (boardValues[5] == 0)
                        { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        else
                        {
                            if (boardValues[7] == 0)
                            { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        }
                    }
                }
            }

            if ((boardValues[2] == tokenBlock) && (boardValues[6] == tokenBlock) && (boardValues[4] == tokenMove))
            {

                if (boardValues[1] == 0)
                { blockList = AddNonDupBlock(blockList, boardValues[1], 1); }
                else
                {
                    if (boardValues[3] == 0)
                    { blockList = AddNonDupBlock(blockList, boardValues[3], 3); }
                    else
                    {
                        if (boardValues[5] == 0)
                        { blockList = AddNonDupBlock(blockList, boardValues[5], 5); }
                        else
                        {
                            if (boardValues[7] == 0)
                            { blockList = AddNonDupBlock(blockList, boardValues[7], 7); }
                        }
                    }
                }


            }

            return blockList;

        }
        private List<int> AddNonDupBlock(List<int> list, int spotBlockingValue, int x)
        {
            if (list.Contains(x))
            { // prevent from dup items being added to the list.

                return list;
            }
            else
            { // only add block spots to the list if they are not dup and empty which is a value of 0 in the list

                if (spotBlockingValue == 0)
                {
                    list.Add(x);
                }

                return list;
            }
        }


        // Produces a list of spots that would trigger a win
        private List<int> FindGameBoardWins(List<int> boardValues, int tokenMove, int tokenBlock)
        {
            // before we get here, the assumpton is that the game is not in the state of win. 
            // generally we should only see 1 item or 0 item in the final list, unless we screwed up on a move

            List<int> winList = new List<int>();

            for (int x = 0; x < 9; x++)
            {

                if (boardValues[x] == tokenMove)
                {
                    if (x == 0)
                    { // corner

                        if ((boardValues[x] == tokenMove) && (boardValues[1] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenMove) && (boardValues[3] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }

                    if (x == 1)
                    { // side

                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenMove) && (boardValues[7] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }

                    if (x == 2)
                    { // corner

                        if ((boardValues[x] == tokenMove) && (boardValues[1] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenMove) && (boardValues[5] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }

                    }

                    if (x == 3)
                    { // side

                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenMove) && (boardValues[5] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }

                    if (x == 4)
                    { // center

                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenMove) && (boardValues[1] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenMove) && (boardValues[7] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenMove) && (boardValues[3] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenMove) && (boardValues[5] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[3], 3); }

                    }

                    if (x == 5)
                    { // side

                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenMove) && (boardValues[3] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                    }

                    if (x == 6)
                    { // corner

                        if ((boardValues[x] == tokenMove) && (boardValues[3] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[3], 3); }
                        if ((boardValues[x] == tokenMove) && (boardValues[7] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }

                    if (x == 7)
                    { // side

                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[8], 8); }
                        if ((boardValues[x] == tokenMove) && (boardValues[8] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[1], 1); }
                        if ((boardValues[x] == tokenMove) && (boardValues[1] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }

                    if (x == 8)
                    { // corner

                        if ((boardValues[x] == tokenMove) && (boardValues[5] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[2], 2); }
                        if ((boardValues[x] == tokenMove) && (boardValues[2] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[5], 5); }
                        if ((boardValues[x] == tokenMove) && (boardValues[7] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[6], 6); }
                        if ((boardValues[x] == tokenMove) && (boardValues[6] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[7], 7); }
                        if ((boardValues[x] == tokenMove) && (boardValues[4] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[0], 0); }
                        if ((boardValues[x] == tokenMove) && (boardValues[0] == tokenMove)) { winList = AddNonDupWin(winList, boardValues[4], 4); }
                    }
                }


            }

            return winList;
        }
        private List<int> AddNonDupWin(List<int> list, int spotToken, int x)
        {
            if (list.Contains(x))
            { // prevent from dup items being added to the list.

                return list;
            }
            else
            { // only add block spots to the list if they are not dup and empty which is a value of 0 in the list

                if (spotToken == 0)
                {
                    list.Add(x);
                }

                return list;
            }
        }


        // Checks to see if the Gameboard is in a state of win by any player. 
        // The sets the winnet details and message in the game class. 
        private TicTacToeGame CheckforWinner(TicTacToeGame Game)
        {
            // { "00","01","02"},
            // { "10","11","12"},
            // { "20","21","22"}
            //
            // win 1 = 00 01 02 (Top left accross)
            // win 2 = 00 10 20 (Top left down)
            // win 3 = 00 11 22 (Diagonal)
            // win 4 = 01 11 21 (Top middle down)
            // win 5 = 02 12 22 (Top right down)
            // win 6 = 10 11 12 (Left middle accross)
            // win 7 = 20 21 22 (Left bottom accross)


            var Check = CheckWin(Game, "X");

            if (Check != "")
            {
                Game.GameOver = true;
                Game.GameWinner = "X";
                return Game;
            }

            Check = CheckWin(Game, "O");

            if (Check != "")
            {
                Game.GameOver = true;
                Game.GameWinner = "O";
                return Game;
            }

            return Game;

        }
        private string CheckWin(TicTacToeGame tic, string playerToken)
        {
            string msg = "";
            if (tic.GameBoardArray[0, 0] == playerToken && tic.GameBoardArray[0, 1] == playerToken && tic.GameBoardArray[0, 2] == playerToken) { msg = "Top Lest Accross"; }
            if (tic.GameBoardArray[0, 0] == playerToken && tic.GameBoardArray[1, 0] == playerToken && tic.GameBoardArray[2, 0] == playerToken) { msg = "Top Left Down"; }
            if (tic.GameBoardArray[0, 0] == playerToken && tic.GameBoardArray[1, 1] == playerToken && tic.GameBoardArray[2, 2] == playerToken) { msg = "Diagonal Left to Right"; }
            if (tic.GameBoardArray[0, 2] == playerToken && tic.GameBoardArray[1, 1] == playerToken && tic.GameBoardArray[2, 0] == playerToken) { msg = "Diagonal Right to Left"; }
            if (tic.GameBoardArray[0, 1] == playerToken && tic.GameBoardArray[1, 1] == playerToken && tic.GameBoardArray[2, 1] == playerToken) { msg = "Top Middle Down"; }
            if (tic.GameBoardArray[0, 2] == playerToken && tic.GameBoardArray[1, 2] == playerToken && tic.GameBoardArray[2, 2] == playerToken) { msg = "Top Right Down"; }
            if (tic.GameBoardArray[1, 0] == playerToken && tic.GameBoardArray[1, 1] == playerToken && tic.GameBoardArray[1, 2] == playerToken) { msg = "Left Middle Accross"; }
            if (tic.GameBoardArray[2, 0] == playerToken && tic.GameBoardArray[2, 1] == playerToken && tic.GameBoardArray[2, 2] == playerToken) { msg = "Left Bottom Accross"; }
            return msg;
        }
        private bool GameBoardTicTacToeDraw(TicTacToeGame tic)
        {

            int spotCount = 0;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (tic.GameBoardArray[x, y] == "X" | tic.GameBoardArray[x, y] == "O") { spotCount++; }

                }
            }

            if (spotCount == 9) { return true; } else { return false; }

        }

    }
}

