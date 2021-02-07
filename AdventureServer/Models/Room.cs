using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class Room
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int N { get; set; }
        public int S { get; set; }
        public int E { get; set; }
        public int W { get; set; }
        public int U { get; set; }
        public int D { get; set; }
        public int RoomPoints { get; set; }

    }
}
