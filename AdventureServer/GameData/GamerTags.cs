using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AdventureServer.GameData
{
    public class GamerTags
    {
        public List<string> GetTags()
        {
            return Tags;
        }

        public string RandomTag()
        {
            return GetTags()[new Random().Next(0, GetTags().Count())];
        }

        public List<string> Tags => new List<string>
            {
                "Player One",
                "Raider",
                "IceBreaker",
                "Iron Giant",
                "Cute Gamer",
                "Wisher",
                "Minecraft Steve",
                "Miner Alex",
                "Acid Washer",
                "Taco Eater",
                "Walk Man",
                "Radio Dial",
                "Fuzz Ball",
                "Fluffy Frog",
                "Mr. Spock",
                "Missing Droid",
                "Super Can",
                "Top Fish",
                "Money Man",
                "Wonder Woman",
                "Time Travler",
                "Wacky Mole",
                "Pac Malt",
                "Gamer Tag Master",
                "Mr. Smith",
                "Walley Mouse",
                "Mickey Mouse",
                "Andy Capp",
                "Top Hatter",
                "Bat Master",
                "Red Winter",
                "Mario Man",
                "Hot Potato",
                "Fed Up Gamer",
                "Faster Player",
                "Timeless Warrior",
                "Player 2.0",
                "Striper Man",
                "Node Killer",
                "Novice Player",
                "Avatar",
                "Avatar Fan",
                "Blue Player",
                "Rogue One",
                "Solo One",
                "Han Fan",
                "Tea Man 2",
                "Cookie Monster",
                "Hunter Killer",
                "Clark W",
                "Hank Hill",
                "Rusty S",
                "The Billdozer",
                "Bobby",
                "Peggy Fan",
                "Don Laundry Rulez!",
                "Hookem Fan",
                "Laundry Dozer",
                "Pizza Eater",
                "Chicken Wing"

            };

  
    }
}
