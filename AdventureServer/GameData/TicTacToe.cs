using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.GameData
{
    public class TicTacToe
    {

        public string EmptyGameBoardString => EmptyGameBoard();


        private string EmptyGameBoard()
        {
            string b = "";

            b += "         |         |         " + "\r\n";
            b += "    1    |    2    |    3    " + "\r\n";
            b += "         |         |         " + "\r\n";
            b += "---------+---------+---------" + "\r\n";
            b += "         |         |         " + "\r\n";
            b += "    4    |    5    |    6    " + "\r\n";
            b += "         |         |         " + "\r\n";
            b += "---------+---------+---------" + "\r\n";
            b += "         |         |         " + "\r\n";
            b += "    7    |    8    |    9    " + "\r\n";
            b += "         |         |         " + "\r\n";

            return b;
        }


        private string HelpMessage()
        {
            return "Welcome to Tic-Tac-Toe.\r\n"
                   + "This is a traditional Tic-Tac-Toe Game. Supporting 0 to 2 human players.\r\n"
                   + "Select a location 1 to 9 and the API will replace the number with the players token. \r\n "
                   + "\r\n"
                   + "We also have some helper command that will return addtional infomation.\r\n"
                   + "HELP  = This message.\r\n"
                   + "VER   = Version of the API"
                   + "CHEAT = provides a list of the best moves. \r\n"
                   + "SWAP  = Swaps players. \r\n"
                   + "#X    = Changes the numbered space to the X token\r\n"
                   + "#O    = Changes the numbered space to the O token.\r\n"
                   + "#Q    = Changes the numbered space back to an empty space.\r\n"
                   + "\r\n"
                   + "The default game is GAME1. \r\n" 
                   + "The AI for the API supports 0 to 3 games variations such as: \r\n"
                   + "GAME1 = Human as X verse Computer as O.\r\n"
                   + "GAME2 = Computer X verse Human as O.\r\n"
                   + "GAME3 = Homan as X verse Human as O.\r\n"
                   + "\r\n";
                   
        }

        private string APIMessage()
        {
            return "API by Steven S. Sparks \r\n"
                + "I hope you enjoy the API. The source is available for Review on GitHub. \r\n";  
        }



    }
}
