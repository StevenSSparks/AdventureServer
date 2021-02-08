using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;
using AdventureServer.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureServer
{
    public class AdventureFramework : IPlayAdventure
    {
        // Game 1 
        private readonly AdventureData.AdventureHouse adventureHouse = new AdventureData.AdventureHouse();

        // storage for the adventures
        private IMemoryCache _gameCache;

        public AdventureFramework(IMemoryCache GameCache)
        {
            _gameCache = GameCache;
        }

        #region Game Cache Management

        private void AddPlayAdventureToCache(PlayAdventure p)
        {

          var cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromMinutes(8 * 60)); //  8 hours

            _ = _gameCache.Set(p.InstanceID, p, cacheEntryOptions);

        }

        private void ReplacePlayAdventuretoCache(PlayAdventure p)
        {
            _gameCache.Remove(p.InstanceID);
            AddPlayAdventureToCache(p);
        }

        private PlayAdventure GetPlayAdventureFromCache(string key)
        {
            var cacheEntry = _gameCache.Get<PlayAdventure>(key);

            return cacheEntry;
        }

        private void RemovePlayAdventureFromCache(string key)
        {
           _gameCache.Remove(key);

        }

        #endregion Game Cache Management 

        #region Instance Management 

        private string SetupNewInstance(int gamechoice)
        {
            var tempId = Guid.NewGuid().ToString();
            if (gamechoice == 1)
            {
                var p = adventureHouse.SetupAdventure(tempId);
                AddPlayAdventureToCache(p);
            }

            return tempId;
        }

        public Boolean CheckInstanceExists(string id)
        {
            var adventure = GetPlayAdventureFromCache(id);

            if (adventure.InstanceID == null) return false;
            return true;
        }

        public PlayAdventure GetInstanceObject(string InstanceId)
        {

            PlayAdventure playAdventure  = GetPlayAdventureFromCache(InstanceId);
            if (playAdventure.InstanceID == null)
            {
                playAdventure.StartRoom = -1;
                playAdventure.WelcomeMessage = "Error: No Instance Found!";
            }
            return playAdventure;
        }

        public Boolean UpdateInstance(PlayAdventure p)
        {
            if (CheckInstanceExists(p.InstanceID))
            {
                ReplacePlayAdventuretoCache(p);
                return true;
            }

            return false;
        }

        public Boolean DeleteInstance(string InstanceID)
        {
            if (CheckInstanceExists(InstanceID))
            {
                RemovePlayAdventureFromCache(InstanceID);
                return true;
            }

            return false;
        }


        #endregion Instance Management

        #region Game Management

        public List<Game> GetGames()
        {
            List<Game> _games = new List<Game>
            {
                new Game {Id =1, Name="Api Adventure House", Ver=".01", Desc="Figure out how to escape from the house."  },
                new Game {Id =1, Name="Adventure House Part 2!", Ver="00", Desc="Exact same game as API Adventure house but using a different name"}
            };

            return _games;
        }

        public GameMoveResult NewGame(int GameID)
        {
            // If the game ID is not in the games list default to 1
            if (!GetGames().Exists(g => g.Id == GameID))
            {
                GameID = 1;
            }

            // setup the game instance 
            var p = GetInstanceObject(SetupNewInstance(GameID).ToString());

            //get the room details for the first room 
            var _room = GetRoom(p.Rooms, p.Player.Room);

            return new GameMoveResult
            {
                InstanceID = p.InstanceID,
                RoomName = _room.Name,
                RoomMessage = p.WelcomeMessage + _room.Desc + " " + GetRoomPath(_room),
                ItemsMessage = GetRoomItemsList(p.Player.Room, p.Items, p.Player.Verbose),
                PlayerName = p.Player.Name,
                HealthReport = GetHealthReport(p.Player.HealthCurrent, p.Player.HealthMax),
            };
          
        }
        #endregion Game Management 

        #region Set Player Points and Health

        private Player SetPlayerPoints(bool isgeneric, string ItemorRoomName, PlayAdventure p)
        {
            Room rm;
            Item item;

            string c;
            c = p.PointsCheckList.Find(t => t.ToString().ToLower() == ItemorRoomName.ToLower());


            if ((c == null) && (isgeneric == true))
            {
                p.Player.Points += 5;
                p.PointsCheckList.Add(ItemorRoomName);
            }
            else
            {

                if (c == null)
                {
                    // If the item or room has not been scored then add the points 

                    rm = p.Rooms.Find(t => t.Name.ToLower() == ItemorRoomName.ToLower());
                    if (rm != null)
                    {
                        p.Player.Points += rm.RoomPoints;
                        p.PointsCheckList.Add(rm.Name);

                    }
                    else
                    {
                        item = p.Items.Find(t => t.Name.ToLower() == ItemorRoomName.ToLower());
                        if (item != null)
                        {
                            p.Player.Points += item.ActionPoints;
                            p.PointsCheckList.Add(item.Name);

                        }
                    }

                }
            }
            return p.Player;
        }

        private string GetPlayerPoints(PlayAdventure p)
        {

            return p.Player.Points.ToString();

        }

        private bool PlayerDead(PlayAdventure p)
        {
            if (p.Player.HealthCurrent < 1) return true;
            return false;
        }
        private int SetPlayerNewHealth(PlayAdventure p)
        {
            return p.Player.HealthCurrent - p.HealthStep;
        }



        #endregion Player Points

        #region Game Object Management 

        public Room GetRoom(List<Room> rooms, int roomnumber)
        {
            return (rooms.FirstOrDefault(t => t.Number == roomnumber));
        }

        public List<Item> MoveItem(List<Item> invitems, string Name, int NewRoom)
        {
            var itemIndex = invitems.FindIndex(t => t.Name.ToLower().Equals(Name.ToLower()));
            var invitem = invitems[itemIndex];
            invitem.Location = NewRoom;
            invitems[itemIndex] = invitem;
            return invitems;
        }

        #endregion Game Object Management

        #region Game Command Parse

        CommandState GetCommandState (GameMove gm)
        {

            return new CommandState();
        }

        private CommandState CheckForSingleWordMove (string direction)
        {
            var cmd = direction.Trim();
            var cs = new CommandState // setup with assumed move details
            {
                Valid = true,
                RawCommand = direction,
                Command = "Go"
            };

            if (cmd.Length == 1)
            {
                if (cmd.Contains("n")) cs.Modifier = "north";
                else if (cmd.Contains("s")) cs.Modifier = "south";
                else if (cmd.Contains("e")) cs.Modifier = "east";
                else if (cmd.Contains("w")) cs.Modifier = "west";
                else if (cmd.Contains("u")) cs.Modifier = "up";
                else if (cmd.Contains("d")) cs.Modifier = "down";
                else
                {
                    cs.Valid = false;
                    cs.Command = "";
                    cs.Modifier = "";
                    cs.Message = "That is not a valid move command.";
                }
            }

            if (cmd.Length == 3)
            {
                if (cmd.Contains("nor")) cs.Modifier = "north";
                else if (cmd.Contains("sou")) cs.Modifier = "south";
                else if (cmd.Contains("eas")) cs.Modifier = "east";
                else if (cmd.Contains("wes")) cs.Modifier = "west";
                else if (cmd.Contains("up")) cs.Modifier = "up";
                else if (cmd.Contains("dow")) cs.Modifier = "down";
                else
                {
                    cs.Valid = false;
                    cs.Command = "";
                    cs.Modifier = "";
                    cs.Message = "That is not a valid move command.";
                }
            }


            return cs;
        }
                
        private CommandState ParseCommand(GameMove gm)
        {
            // Parse the command
            // take into account messy typing with extra spaces front, back and between
            // use the firt two words   wave  wand  and wave wand should parse the same 
            var c = gm.Move.Trim().ToLower();
            var cList = c.Split(" ").ToList<string>();
            var cmds = new List<string>();

            //take the first 2 that are not empty 
            int cnt = 0;
            foreach(string s in cList)
            {
                if (s.Trim() != "") { cmds.Add(s.ToLower()); cnt++; }
                if (cnt == 2) { break; }
            }

            var cs = new CommandState();
            if (cmds.Count() > 1)
            {
                cs.RawCommand = gm.Move;
                cs.Command = cmds[0];
                cs.Modifier = cmds[1];
            }
            else
            {
                cs.RawCommand = gm.Move;
                cs.Command = cmds[0];
            }

            return cs;

        }


        // Actions result in the player completing and activity
        // Actions that are a single word are a command or a short move
        // Short moves are 1 to 3 characters, commands are > 3 characters
        // All actions need to have 2 part such as eat pie (action item)
        // actions have to match the permitted action assocated with an item

        // First we will check for Command [Actions]







        #endregion Game Command Parse

        #region Primary Game Command Processing

        public GameMoveResult GameMove(GameMove move)
        {

            if (CheckInstanceExists(move.InstanceID))
            {
                var gmr = ProcessMove(move);
                return gmr;
            }
            else return new GameMoveResult
            {
                InstanceID = "-1",
                ItemsMessage = "",
                RoomMessage = "Game does not exist. Please begin again."
            };

        }

        private GameMoveResult ProcessMove(GameMove move)
        {
            var cs = new CommandState();

            // Get the Instance we need to process 
            var p = GetInstanceObject(move.InstanceID);

            //setup the intial response message and result
            string actionMessage = "You can't do that here.\r\n";

            var gmr = new GameMoveResult
            {
                InstanceID = p.InstanceID,
                RoomName = GetRoom(p.Rooms, p.Player.Room).Name,
                RoomMessage = GetRoom(p.Rooms, p.Player.Room).Desc,
                PlayerName = p.Player.Name,
                ItemsMessage = GetRoomItemsList(p.Player.Room, p.Items, true),
            };

            //parse the command 
            cs = ParseCommand(move);

            if (cs.Command == "go")
            {
                // if the player types go north - verse n or nor we will parse and convert to the short command
                var ml = new List<string> { "north", "south", "east", "west", "up", "down" };
                if (ml.Contains(cs.Modifier))
                {
                    cs.Command = cs.Modifier.Substring(0, 1);
                }
            }

            // shortcut for moves
            if ((cs.Command.Length >= 1) & (cs.Command.Length <= 3))
            {
                // here we will process all moves 
                cs = CheckForSingleWordMove(cs.Command);

                if (cs.Valid == true)
                {
                    actionMessage = "move!!!";
                    return PostActionUpdate(gmr, p, actionMessage);
                }
                else
                {
                    //adjust the result 
                    return PostActionUpdate(gmr, p, cs.Message);
                }
            }

            // Determine Move Type 
            // Perform Move Action
            // Return Move Result 

            return PostActionUpdate(gmr, p, actionMessage);

        }

        private GameMoveResult PostActionUpdate(GameMoveResult gmr, PlayAdventure p, string actionMessage)
        {
            var actiongmr = gmr;
            actiongmr.HealthReport = GetHealthReport(p.Player.HealthCurrent, p.Player.HealthMax);

            if (actiongmr.HealthReport == "Dead")
            {
                actiongmr.RoomMessage = "You Died. R.I.P.\r\n" + gmr.RoomMessage;
                actiongmr.ItemsMessage = "You see a SIGN\r\n";
                return actiongmr;
            }

            if (actiongmr.HealthReport == "Bad!")
            {
                actiongmr.RoomMessage = "You are feeling hungry and ill. \r\n" + gmr.RoomMessage;
            }

            p.Player = SetPlayerPoints(false, gmr.RoomName, p);
            actiongmr.RoomMessage = actionMessage + "\r\n" + actiongmr.RoomMessage;
            actiongmr.RoomMessage = actiongmr.RoomMessage + GetRoomPath(GetRoom(p.Rooms, p.Player.Room));

            UpdateInstance(p);
            return actiongmr;
        }

        private GameMoveResult QuitGame(GameMove gm)
        {
            GameMoveResult gmr = new GameMoveResult();

            if (DeleteInstance(gm.InstanceID))
            {
                gmr.InstanceID = "-1";
                gmr.ItemsMessage = "";
                gmr.RoomMessage = "Game Over";
            }
            else
            {
                gmr.ItemsMessage = "Please Close the Client";
                gmr.RoomMessage  = "Delete Failed. Looks like you are stuck.";
            };

            return gmr;
        }

        #endregion Primary Game Command Processing

        #region Game Validation Methods





        private Boolean DirectionOK(Room room, string direction)
        {
            switch (direction)
            {
                case "North":
                    if (room.N < 99) return true;
                    break;
                case "South":
                    if (room.S < 99) return true;
                    break;
                case "East":
                    if (room.E < 99) return true;
                    break;
                case "West":
                    if (room.W < 99) return true;
                    break;
                case "Up":
                    if (room.U < 99) return true;
                    break;
                case "Down":
                    if (room.D < 99) return true;
                    break;
            }
            return false;
        }




        #endregion Game Validation Methods

        #region String Generation Methods

        private string RoomInventory(int room, List<Item> Items)
        {
            string _itemstring = "";
            int count = 0;

            foreach (Item i in Items)
            {

                if (i.Location == room)
                {
                    if (count > 0) _itemstring += ", ";
                    _itemstring += i.Name;
                    count++;
                }

            }

            if (count == 0) _itemstring = "No Items";

            return _itemstring;

        }

        private string GetRoomItemsList(int RoomNumber, List<Item> Items, bool verbose)
        {
            string _result = RoomInventory(RoomNumber, Items);

            if (verbose) { return "You see: " + _result; }
            else
            {
                if (_result == "No Items") { return ""; }
                else { return "You see: " + _result; }

            }
        }

        private string GetRoomPath(Room rm)
        {
            // gets the available list of path ouf of a room and returns a string with the path. 

            string _result = "";

            int pc = 0;  // position count
            int cs = 0; // comman seperator 
            int ac = 99; // if the and counter is not 99 then we won't add a comma

            // in the data if the room is 99 then its not a valid direction

            if (rm.N != 99) { pc++; };
            if (rm.S != 99) { pc++; };
            if (rm.E != 99) { pc++; };
            if (rm.W != 99) { pc++; };
            if (rm.U != 99) { pc++; };
            if (rm.D != 99) { pc++; };

            // A room could have no exit. You can "teleport" in a room with not exits 
            if (pc == 0) { return ""; }

            // construct the list of directions you can follow from a room 
            if (pc > 1) { ac = pc - 1; }

            if (rm.N != 99) { cs++; _result += "north"; }
            if (ac == cs) { ac = 99; _result += " and "; }
            if ((ac != 99) && (pc > 2) && (cs > 0)) { _result += ", "; }

            if (rm.S != 99) { cs++; _result += "south"; }
            if (ac == cs) { ac = 99; _result += " and "; }
            if ((ac != 99) && (pc > 2) && (cs > 0)) { _result += ", "; }

            if (rm.E != 99) { cs++; _result += "east"; }
            if (ac == cs) { ac = 99; _result += " and "; }
            if ((ac != 99) && (pc > 2) && (cs > 0)) { _result += ", "; }

            if (rm.W != 99) { cs++; _result += "west"; }
            if (ac == cs) { ac = 99; _result += " and "; }
            if ((ac != 99) && (pc > 2) && (cs > 0)) { _result += ", "; }

            if (rm.U != 99) { cs++; _result += "up"; }
            if (ac == cs) { ac = 99; _result += " and "; }
            if ((ac != 99) && (pc > 2) && (cs > 0)) { _result += ", "; }

            if (rm.D != 99) { _result += "down"; }

            return "You can go " + _result + " from here.\r\n";

        }

        private string GetHealthReport(int current, int max)
        {
            double hp = (double)current / max;

            if (hp >= .7) { return "Great!"; }
            else if (hp >= .5) { return "Okay"; }
            else if (hp >= .3) { return "Bad!"; }
            else if (hp >= .1) { return "Horriable!!"; }
            else return "Dead";
        }

        private string GetItemDesc(string name, List<Item> Items)
        {
            var _result = Items.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).Description.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        private string GetItemAction(string name, List<Item> Items)
        {
            var _result =Items.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).Action.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        private static string GetItemActionVerb(string name, List<Item> Items)
        {
            var _result = Items.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).ActionVerb.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        private string GetWrongDirection(List<Message> messages, string dir)
        {
            string _message = "";
            string _netural = "";

            List<Message> _querymesssages;

            Random r = new Random();

            List<string> card_directions = new List<string> { "n", "north", "s", "south", "e", "east", "w", "west" };

            if (dir.ToLower() == "up")
            {
                _querymesssages = messages.FindAll(t => t.MessageTag == "Netural").ToList();
                _netural = _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
                _querymesssages.Clear();
                _querymesssages = _querymesssages = messages.FindAll(t => t.MessageTag == "Up").ToList();
                _message = _netural + "\r\n" + _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
            }
            else if (dir.ToLower() == "down")
            {
                _querymesssages = messages.FindAll(t => t.MessageTag == "Netural").ToList();
                _netural = _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
                _querymesssages.Clear();
                _querymesssages = _querymesssages = messages.FindAll(t => t.MessageTag == "Down").ToList();
                _message = _netural + "\r\n" + _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
            }
            else if (card_directions.Contains(dir.ToLower()))
            {
                _querymesssages = messages.FindAll(t => t.MessageTag == "Netural").ToList();
                _netural = _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
                _querymesssages.Clear();
                _querymesssages = _querymesssages = messages.FindAll(t => t.MessageTag == "Any").ToList();
                _message = _netural + "\r\n" + _querymesssages[r.Next(0, _querymesssages.Count - 1)].Messsage;
            }

            if (_message.Contains("@")) { return _message.Replace("@", dir).ToString(); }
            else if (_message != "") { return _message; }

            return "You can't do that here.";

        }

        #endregion String Generation Mthods

        #region Game Actions to Activities

        #endregion Game Actions to Activities


    }
}

