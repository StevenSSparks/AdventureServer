using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;
using AdventureServer.AdventureData;

namespace AdventureServer.Storage
{
    public static class DataClass
    {
        // memory storage for each instance of the adventure game
        // consinder making this a cache later 
        public static List<PlayAdventure> AdventureGameInstances { get; set; } = new List<PlayAdventure>();
        public static AdventureHouse AdventureHouse { get; } = new AdventureHouse();

    }
}
