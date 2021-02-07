using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;
using AdventureServer.AdventureData;

namespace AdventureServer.AdventureData
{
    public class AdventureHouse
    {
        public string GetAdventureHelpText()
        {
            return "You pause and recall a bedtime story your mother use to tell you: \r\n"
                   + "Once upon a time a great explorer wondered into a mystery house. "
                   + "The adventurer visited rooms from the EAST to the WEST. "
                   + "Going UP and DOWN stairs and ladders looking for items. The hero "
                   + "took many actions such as LOOKing and GETting item found in their path. "
                   + "From time to time the hero would USE these items to explore further. "
                   + "Sometimes these things would need to used in a specific way. "
                   + "Fortunately for the adventurer, their backpack has infinite INVentory "
                   + "space and they could carry almost anything. The adventure seemed never "
                   + "to end unless a specific exit or item was found. There were times when "
                   + "the adventurer would die, but since this is a story you can always "
                   + "RESTART from the beginning. The hero would often get and need to EAT "
                   + "a snack. The rest of the story fades from your mind, but you do recall "
                   + "your mom talking about the explorer who would WAVE things "
                   + "while EATing an apple. \r\n";
        }

        public PlayAdventure SetupAdventure(string gameid)
            { 

                var _gamerTag = new GamerTags().RandomTag();

                var _play = new PlayAdventure
                {
                    GameID = 1,
                    GameName = "Adventure House 2.0",
                    InstanceID = gameid,
                    StartRoom = 20,
                    WelcomeMessage = "___________________________\r\n\r\n" +
                                     " Dear " + _gamerTag + ",\r\n\r\n" +
                                     " This is a simple 2 word\r\n" +
                                     " adventure game. Use simple\r\n" +
                                     " but HELPful commands to \r\n" +
                                     " find your way out.\r\n\r\n" +
                                     "       Good Luck! \r\n" +
                                     "        The Management.\r\n\r\n" +
                                     "___________________________\r\n\r\n\r\n",
                    MaxHealth = 200,
                    HealthStep = 2,
                    Items = Items(),
                    Messages = Messages(),
                    Rooms = Rooms(),
                    Player = new Player { Name = _gamerTag, PlayerID = gameid, HealthMax = 200, HealthCurrent = 200, Room = 20, Steps = 2, Verbose = true, Points = 1 },
                    GameActive = true,
                    PointsCheckList = new List<string> { "NewGame" }
                };

                return _play;
            }


            private List<Item> Items()
            {

                var _items = new List<Item>
            {
                new Item { Name="BREAD",  Description="A small loaf of bread. Not quite a lunch, too big for a snack.",Location=06, Action="The bread was fresh and warm.", ActionVerb = "EAT", ActionResult="HEALTH", ActionValue = "100", ActionPoints = 5},
                new Item { Name="BUGLE", Description="You were never very good with instruments.",Location= 20, Action="You try to no avail to produce something that could constitute music.", ActionVerb ="USE", ActionResult = "HEALTH", ActionValue = "-1" , ActionPoints=1 },
                new Item { Name="APPLE",Description= "A nice, red fruit that looks rather apetizing.", Location = 07, Action="Tastes just as good as it looked.", ActionVerb = "EAT", ActionResult = "HEALTH", ActionValue = "25", ActionPoints = 10 },
                new Item { Name="KEY",Description= "A shiny, aesthetically pleasing key. Must open something.", Location = 24, Action= "The key fits perfectly and the door unlocked with some effort.", ActionVerb="USE", ActionResult = "UNLOCK", ActionValue = "1|E|0|This is the entrance. The door is unlocked.|Door is now unlocked", ActionPoints=100},
                new Item { Name="WAND", Description= "A small wooden wand.",Location = 17, Action="You wave the wand and the room fades for a second.", ActionVerb="WAVE", ActionResult = "TELEPORT", ActionValue = "1", ActionPoints=10 },
                new Item { Name="PIE", Description= "A small slice of apple pie. Mouthwatering.",Location = 10, Action= "A little cold, but there never really a good reason to turn down pie.", ActionVerb="EAT", ActionResult="HEALTH", ActionValue ="100", ActionPoints = 10 },
                new Item { Name="BREAD",  Description="A small loaf of bread. Not quite a lunch, too big for a snack.",Location=99, Action="It's too big for a snack. Maybe later, for lunch.", ActionVerb = "EAT", ActionResult="HEALTH", ActionValue = "100", ActionPoints= 10 },
                new Item { Name="BUGLE", Description="You were never very good with instruments.",Location= 99, Action="You try to no avail to produce something that could constitute music.", ActionVerb ="USE", ActionResult = "HEALTH", ActionValue = "-1", ActionPoints =1 },
                new Item { Name="APPLE",Description= "A nice, red fruit that looks rather apetizing.", Location = 99, Action="Tastes just as good as it looked.", ActionVerb = "EAT", ActionResult = "HEALTH", ActionValue = "25", ActionPoints=25 },
                new Item { Name="KEY",Description= "A shiny, aesthetically pleasing key. Must open something.", Location = 99, Action= "The key fits perfectly and the door unlocked with some effort.", ActionVerb="USE", ActionResult = "UNLOCK", ActionValue = "1|E|0|This is the entrance. The door is unlocked.|Door is now unlocked" },
                new Item { Name="WAND", Description= "A small wooden wand.",Location = 99, Action="You wave the wand and the room fades for a second.", ActionVerb="WAVE", ActionResult = "TELEPORT", ActionValue = "1", ActionPoints=1},
                new Item { Name="PIE", Description= "A small slice of apple pie. Mouthwatering.",Location = 99, Action= "A little cold, but there never really a good reason to turn down pie.", ActionVerb="EAT", ActionResult="HEALTH", ActionValue ="100", ActionPoints=10},
                new Item { Name="STICK", Description= "This is the developers helpful and magic stick.",Location = 00, Action= "This looks a lot a debugging tool that a developer would create to make his life easy.", ActionVerb="WAVE", ActionResult="TELEPORT", ActionValue ="99", ActionPoints=0},
                new Item { Name="KITTEN", Description= "A delightful kitten",Location = 20, Action= "The little fuzzball of a black and white kitten just looks so adorable!", ActionVerb="PET", ActionResult="FOLLOW", ActionValue ="20", ActionPoints=50 },
                new Item { Name="ROCK", Description= "A magic rock",Location = 19, Action= "This looks more like poop than a rock. Might want to get rid of this thing soon.", ActionVerb="THROW", ActionResult="TELEPORT", ActionValue ="20", ActionPoints=10 }
           };

                return _items;
            }

            private List<Room> Rooms()
            {
                var _rooms = new List<Room>
            {
                new Room { Number = 00, RoomPoints=100 ,Name = "Exit!", Desc="Exit! - You have found freedom!", N=99, S=99, E=99, W=01, U=99, D=99 },
                new Room { Number = 01, RoomPoints=25 ,Name = "Entrance Hall", Desc="This is the entrance. The door is locked.", N=10, S=02, E=99, W=99, U=99, D=99 },
                new Room { Number = 02, RoomPoints=50 ,Name = "Downstairs Hall", Desc="This hall is at the bottom of the stairs.", N=01, S=04, E=03, W=99, U=11, D=99 },
                new Room { Number = 03, RoomPoints=5 ,Name = "Guest Bathroom", Desc="Small guest bathroom downstairs.", N=99, S=99, E=05, W=02, U=99, D=99 },
                new Room { Number = 04, RoomPoints=5 ,Name = "Living Room", Desc="This is a simple living room on the southeast side.", N=02, S=99, E=99, W=05, U=99, D=99 },
                new Room { Number = 05, RoomPoints=5 ,Name = "Family Room", Desc = "An inviting family room with a large TV.", N=06, S=99, E=02, W=99, U=99, D=99 },
                new Room { Number = 06, RoomPoints=5 ,Name = "Nook", Desc="This is a nook with a small dining table", N=07, S=05, E=99, W=24, U=99, D=99 },
                new Room { Number = 07, RoomPoints=5 ,Name = "Kitchen", Desc="There is a clean floor and a large granite counter.", N=08, S=06, E=99, W=99, U=99, D=99 },
                new Room { Number = 08, RoomPoints=5 ,Name = "Kitchen Hall", Desc="This is a small hall with two large trash cans that are empty.", N=99, S=07, E=10, W=09, U=99, D=99 },
                new Room { Number = 09, RoomPoints=5 ,Name = "Garage", Desc="The the big garage door is closed. The garage is empty and clean.", N=99, S=99, E=08, W=99, U=99, D=99 },
                new Room { Number = 10, RoomPoints=5 ,Name = "Dining Room", Desc = "The dining room is on the northeast side.", N = 99, S = 01, E = 99, W = 08, U = 99, D = 99 },
                new Room { Number = 11, RoomPoints=5 ,Name = "Upstairs Hall", Desc = "This hall is at the top of the stairs.", N = 99, S = 12, E = 16, W = 13, U = 99, D = 02 },
                new Room { Number = 12, RoomPoints=5 ,Name = "Upper East Hall", Desc = "Hall with two tables and computers. The computers are broken.", N = 11, S = 15, E = 99, W = 99, U = 99, D = 99 },
                new Room { Number = 13, RoomPoints=5 ,Name = "Upper North Hall", Desc = "The hall has a large closet. ", N = 18, S = 14, E = 11, W = 17, U = 99, D = 99 },
                new Room { Number = 14, RoomPoints=5 ,Name = "Upper West Hall", Desc = "The hall with a small closet. That is nailed shut with an out of order sign on the door. ", N = 13, S = 23, E = 99, W = 22, U = 99, D = 99 },
                new Room { Number = 15, RoomPoints=5 ,Name = "Guest Room", Desc = "The bedroom has a queen size bed. The bed is covered in roses that look a bit old. The petels like they were place there months ago.", N = 12, S = 99, E = 99, W = 99, U = 99, D = 99 },
                new Room { Number = 16, RoomPoints=5 ,Name = "Laundry", Desc = "This is a laundry room with a washer and dryer. The dryer looks fine but the washer looks rusty and old.", N = 99, S = 99, E = 99, W = 11, U = 99, D = 99 },
                new Room { Number = 17, RoomPoints=5 ,Name = "Main Bathroom", Desc = "The main bathroom with a large bathtub that is full of bubble bath. The water looks dirty.", N = 99, S = 99, E = 13, W = 99, U = 99, D = 99 },
                new Room { Number = 18, RoomPoints=5 ,Name = "Master Bedroom", Desc = "The master bedroom with an inviting king size bed. The room is messy and it seems like they had a party here.", N = 21, S = 13, E = 19, W = 99, U = 99, D = 99 },
                new Room { Number = 19, RoomPoints=5 ,Name = "Master Closet", Desc = "This is a long and narrow walk-in closet. A good place to put stairs to an attic.", N = 99, S = 99, E = 99, W = 18, U = 20, D = 99 },
                new Room { Number = 20, RoomPoints=0 ,Name = "Attic", Desc = "You are in the Adventure house attic. This room is smelly and dark.", N = 99, S = 99, E = 99, W = 99, U = 99, D = 19 },
                new Room { Number = 21, RoomPoints=5 ,Name = "Master BathRoom", Desc = "Warm master bedroom with a shower and tub.", N = 99, S = 18, E = 99, W = 99, U = 99, D = 99 },
                new Room { Number = 22, RoomPoints=5 ,Name = "Children's Room", Desc = "Clean children's room with twin beds.", N = 99, S = 99, E = 14, W = 99, U = 99, D = 99 },
                new Room { Number = 23, RoomPoints=5 ,Name = "Entertainment Room", Desc = "This is a very inviting play room with games and toys.", N = 14, S = 99, E = 99, W = 99, U = 99, D = 99 },
                new Room { Number = 24, RoomPoints=50 ,Name = "Patio", Desc = "You are standing on a finely crafted wooden patio. A key on the floor.", N = 99, S = 99, E = 06, W = 99, U = 99, D = 99 },
                new Room { Number = 93, RoomPoints=50 ,Name = "Glowing Ladder", Desc = "You are on a what seems like and endless glowing ladder. You see magic spiraling vortex. ", N = 99, S = 99, E = 19, W = 99, U = 95, D = 94 },
                new Room { Number = 94, RoomPoints=50 ,Name = "Mystery Ladder", Desc = "You climbed on to the ladder and your memory of how to get back fades. You are on a what seems like and endless magic ladder.", N = 99, S = 99, E = 99, W = 99, U = 93, D = 95 },
                new Room { Number = 95, RoomPoints=50 ,Name = "Magic Room", Desc = "A magic room. The walls sparkle and shine. This room seems like a very happy place. You see 4 doors and ladders leading up and down", N = 20, S = 20, E = 20, W = 20, U = 94, D = 93 }

            };

                return _rooms;
            }

            private List<Message> Messages()
            {
                var _messsages = new List<Message>
            {
                new Message {MessageTag ="Netural", Messsage="There is no way to go @ from here." },
                new Message {MessageTag ="Netural", Messsage="You can't go @." },
                new Message {MessageTag ="Netural", Messsage="There's nothing @ from here." },
                new Message {MessageTag ="Up", Messsage="You can't go through the roof." },
                new Message {MessageTag ="Up", Messsage="There's a roof in the way." },
                new Message {MessageTag ="Up", Messsage="Ouch! You bump your head!"},
                new Message {MessageTag ="Down", Messsage="You can't dig through the floor." },
                new Message {MessageTag ="Down", Messsage="You sadly aren't a mole." },
                new Message {MessageTag ="Down", Messsage="You begin to dig but stop out of frustration." },
                new Message {MessageTag ="Any", Messsage="There's no way to go @." },
                new Message {MessageTag ="Any", Messsage="There's no path leading @ from here." },
                new Message {MessageTag ="Any", Messsage="It's a bad idea to go @. That leads to trouble."}
            };

                return _messsages;
            }
    }
}
