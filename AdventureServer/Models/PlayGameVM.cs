using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class PlayGameVM
    {
        public string GameID { get; set; }
        public string GamerTag { get; set; }
        public string RoomName { get; set; }
        public string Buffer { get; set; }
        public string Message { get; set; }
        public string Items { get; set; }
        public string HealthReport { get; set; }

    }
}
