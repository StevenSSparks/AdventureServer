using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Models
{
    public class CommandState
    {
        public bool Valid { get; set; }
        public string RawCommand { get; set; }
        public string Command { get; set; }
        public string Modifier { get; set; }
        public string Message { get; set; }

    }
}
