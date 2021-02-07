using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Location { get; set; }  // valid locations are room number, 9998 for used and 9999 for inventory of player
        public string Action { get; set; }
        public int ActionPoints { get; set; }
        public string ActionVerb { get; set; }
        public string ActionResult { get; set; }
        public string ActionValue { get; set; }
    }
}
