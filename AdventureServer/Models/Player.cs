using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string PlayerID { get; set; }
        public int Room { get; set; }
        public int HealthCurrent { get; set; }
        public int HealthMax { get; set; }
        public int Steps { get; set; }
        public bool Verbose { get; set; }
        public int Points { get; set; }
        

    }
}
