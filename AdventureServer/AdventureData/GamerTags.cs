using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AdventureServer.AdventureData
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
                "Brian Part-2",
                "Zar Ruba",
                "Wer Ner",
                "Sparks Fly",
                "Dev Ops",
                "Brian Boitano",
                "Buzz Lightyear",
                "Ham Radio Operator",
                "Mr. T",
                "Mrs. T",
                "Dr Smith",
                "Alvin",
                "Simon",
                "Person X"

            };

  
    }
}
