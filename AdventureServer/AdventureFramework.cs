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

        private readonly AdventureData.AdventureHouse adventureHouse = new AdventureData.AdventureHouse();

        // storage for the adventures
        private IMemoryCache _gameCache;

        public AdventureFramework(IMemoryCache GameCache)
        {
            _gameCache = GameCache;
        }

        #region Game Cache Management

        public void AddPlayAdventureToCache(PlayAdventure p)
        {

          var cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromMinutes(8 * 60)); //  8 hours

            _ = _gameCache.Set(p.InstanceID, p, cacheEntryOptions);

        }

        public PlayAdventure GetPlayAdventureFromCache(string key)
        {
            var cacheEntry = _gameCache.Get<PlayAdventure>(key);

            return cacheEntry;
        }

        public void RemovePlayAdventureFromCache(string key)
        {
           _gameCache.Remove(key);

        }

        #endregion Game Cache Management 

        #region Instance Management 

        public string SetupNewInstance(int gamechoice)
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


        public Boolean DeleteInstance(PlayAdventure p)
        {
            if (CheckInstanceExists(p.InstanceID))
            {
                RemovePlayAdventureFromCache(p.InstanceID);
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
            var adventuregame = GetInstanceObject(SetupNewInstance(GameID).ToString());

            //get the room details for the first room 
            var _room = GetRoom(adventuregame.Rooms, adventuregame.Player.Room);

            return new GameMoveResult
            {
                InstanceID = adventuregame.InstanceID,
                RoomName = _room.Name,
                RoomMessage = adventuregame.WelcomeMessage + _room.Desc + " " + GetRoomPath(_room),
                ItemsMessage = RoomItemsList(adventuregame.Player.Room, adventuregame.Items, adventuregame.Player.Verbose),
                PlayerName = adventuregame.Player.Name
         
            };
          
        }
        #endregion Game Management 

        #region Player Points

        public Player SetPlayerPoints(bool isgeneric, string ItemorRoomName, PlayAdventure p)
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

        public static string GetPlayerPoints(PlayAdventure p)
        {

            return p.Player.Points.ToString();

        }

        #endregion Player Points

        #region Game Object Management 

        public string RoomItemsList(int RoomNumber, List<Item> Items, bool verbose)
        {
            string _result = GetRoomInventory(RoomNumber, Items);

            if (verbose) { return "You see: " + _result; }
            else
            {
                if (_result == "No Items") { return ""; }
                else { return "You see: " + _result; }

            }
        }

        public Room GetRoom(List<Room> rooms, int roomnumber)
        {
            return (rooms.FirstOrDefault(t => t.Number == roomnumber));
        }


        public string GetRoomInventory(int room, List<Item> Items)
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

        public List<Item> MoveItem(List<Item> invitems, string Name, int NewRoom)
        {
            var itemIndex = invitems.FindIndex(t => t.Name.ToLower().Equals(Name.ToLower()));
            var invitem = invitems[itemIndex];
            invitem.Location = NewRoom;
            invitems[itemIndex] = invitem;
            return invitems;
        }

        public static string GetItemDesc(string name, List<Item> invItems)
        {
            var _result = invItems.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).Description.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        public static string GetItemAction(string name, List<Item> invItems)
        {
            var _result = invItems.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).Action.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        public static string GetItemActionVerb(string name, List<Item> invItems)
        {
            var _result = invItems.FirstOrDefault(t => t.Name.ToLower().Equals(name.ToLower())).ActionVerb.ToString();
            if (_result == null) { _result = ""; }
            return _result;
        }

        #endregion Game Object Management

        #region Game Movement - Navigation

        public string GetRoomPath(Room rm)
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

        public static string GetWrongDirection(List<Message> messages, string dir)
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



        #endregion Game Movement 

        #region Game Command Processing

        public GameMoveResult GameMove(GameMove move)
        {




            return new GameMoveResult
            {
                InstanceID = move.InstanceID
            };
        }


        #endregion Game Command Processing

    }
}

