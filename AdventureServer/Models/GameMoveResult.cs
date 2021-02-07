using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class GameMoveResult
    {
        public long InstanceID { get; set; }
        public string RoomName { get; set; }
        public string RoomMessage { get; set; }
        public string ItemsMessage { get; set; }
        public string HealthReport { get; set; }

    }
}
