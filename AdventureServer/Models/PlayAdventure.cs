using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class PlayAdventure
    {
        // TODO: add a lifetime for this object so we can clear out the instances or use a cache
        public int GameID;
        public string GameName;
        public string InstanceID;
        public string WelcomeMessage;
        public int StartRoom;
        public int MaxHealth;
        public int HealthStep;
        public Player Player;
        public List<Item> Items;
        public List<Message> Messages;
        public List<Room> Rooms;
        public Boolean GameActive = false;
        public List<string> PointsCheckList;

    }
}
