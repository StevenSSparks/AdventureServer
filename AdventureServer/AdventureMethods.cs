using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Models;
using AdventureServer.Storage;


namespace AdventureServer
{
    public class AdventureMethods
    {

        public List<Game> GetGames()
        {
            List<Game> _games = new List<Game>
            {
                new Game {Id =1, Name="Api Adventure House", Ver=".01", Desc="Figure out how to escape from the house."  },
                new Game {Id =1, Name="Adventure House Part 2!", Ver="00", Desc="Exact same game as API Adventure house but using a different name"}
            };

            return _games;
        }

        #region Instance Management 

        public Guid SetupNewInstance(int gamechoice)
        {
            var tempId = new Guid();
            if (gamechoice == 1)
            {
                var p = DataClass.AdventureHouse.SetupAdventure(tempId.ToString());
                DataClass.AdventureGameInstances.Add(p);
            }
            return tempId;
        }

        public static Boolean CheckInstanceExists(string id)
        {
            PlayAdventure playAdventure = DataClass.AdventureGameInstances.Find(t => t.InstanceID ==  id);
            if (playAdventure == null) return false;
            return true;
        }

        public static PlayAdventure GetInstanceObject(string InstanceId)
        {

            PlayAdventure playAdventure = DataClass.AdventureGameInstances.FirstOrDefault(t => t.InstanceID == InstanceId);
            if (playAdventure == null)
            {
                playAdventure.StartRoom = -1;
                playAdventure.WelcomeMessage = "Error: No Instance Found!";
            }
            return playAdventure;
        }

        public bool ResetInstance(int gamechoice, string id)
        {
            if (gamechoice == 1)
            {
                var p = DataClass.AdventureHouse.SetupAdventure(id);
                DataClass.AdventureGameInstances.Add(p);
                return true;
            }

            else return false;
               
        }



        public Boolean UpdateInstance(PlayAdventure p)
        {
            if (CheckInstanceExists(p.InstanceID))
            {
                int index = DataClass.AdventureGameInstances.FindIndex(t => t.InstanceID == p.InstanceID);
                DataClass.AdventureGameInstances[index] = p;
                return true;
            }

            return false;
        }

        public static Boolean DeleteInstance(PlayAdventure p)
        {
            if (CheckInstanceExists(p.InstanceID))
            {
                int index = DataClass.AdventureGameInstances.FindIndex(t => t.InstanceID == p.InstanceID);
                DataClass.AdventureGameInstances.RemoveAt(index);
                return true;
            }

            return false;
        }


        #endregion Instance Management







    }
}
