using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class PlayAdventure
    {
        // TODO: add a lifetime for this object so we can clear out the instances or use a cache
        public int GameID { get; set; }
        public string GameName { get; set; }
        public string GameHelp { get; set; }
        public string InstanceID { get; set; }
        public string WelcomeMessage { get; set; }
        public int StartRoom { get; set; }
        public int MaxHealth { get; set; }
        public int HealthStep { get; set; }
        public Player Player { get; set; }
        public List<Item> Items { get; set; }
        public List<Message> Messages { get; set; }
        public List<Room> Rooms { get; set; }
        public Boolean GameActive { get; set; } = false;
        public List<string> PointsCheckList { get; set; }

    }
}
