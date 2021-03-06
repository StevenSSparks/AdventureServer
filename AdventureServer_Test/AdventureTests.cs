using AdventureServer;
using AdventureServer.Controllers;
using AdventureServer.Interfaces;
using AdventureServer.Models.AdventureGame;
using AdventureServer.Services.TextAdventureGameService;
using AdventureServer.Services.GameCacheService;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using AdventureServer.Services.AppVersionService;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureServer_Test
{
    public class AdventureTests
    {

        private readonly MemoryCacheOptions mco = new MemoryCacheOptions();
        private IMemoryCache _memoryCache;
        private IGameCache _gameCache;
        private WelcomeController _welcomeSontroller;
        private TextAdventureGameService _adventureFramework;
        private AdventureController _adventureController;
        // private PlayAdventureController _playAdventureController;

        [SetUp]
        public void Setup()
        {
            _memoryCache = new MemoryCache(mco);
            _gameCache = new GameCacheService(_memoryCache);
            _welcomeSontroller = new WelcomeController(new AppVersionService());
            _adventureFramework = new TextAdventureGameService(_gameCache);
            _adventureController = new AdventureController(_adventureFramework);
           // _playAdventureController = new PlayAdventureController(_adventureController);

        }

        [Test]
        public void WelcomeControllerTest()
        {
            var OkResult = _welcomeSontroller.Index();
            Assert.IsNotNull(OkResult);
        }

        //[Test]
        //public void PlayAdventureControllerWelcomeTest()
        //{
        //    TODO: Figure out how to call http endpoint - need to manage the session variable and httpcontext 
        //  
        //    var OkResult = _playAdventureController.Welcome();
        //    Assert.IsNotNull(OkResult);
        //}


        [Test]
        public void TestCreateNewGame()
        {
            var getadv = _adventureFramework.ControllerEntry_NewGame(1);
            Assert.IsTrue(getadv.InstanceID.Length > 10);

        }

        [Test]
        public void TestItemMovemementAndCache()
        {
            // Start Game
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            PlayAdventure adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            // Get Item
            var gameMoveResult = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "get bugle" });
            // Save the Game State
            _adventureFramework.GameInstance_Update(adv);
            // Load Game from Cache
            adv = _adventureFramework.GameInstance_GetObject(adv.InstanceID);
            // Check to see if the item is in the player inventory 
            var itemloc = adv.Items.Find(i => i.Name.ToLower() == "bugle").Location;
            Assert.IsTrue(itemloc == 9999);

        }

        [Test]
        public void PlayTestFirstRoom()
        {
            // Enter the Game a pick up the first item
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            PlayAdventure adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            var gameMoveResult = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "get bugle" });
            Assert.IsTrue(!gameMoveResult.ItemsMessage.Contains("BUGLE"));
        }

        [Test]
        public void PlayTestFirstRoomPetKitten()
        {
            // Enter the Game, Pet the Kitten and then see if the Kitten follows the player 
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            PlayAdventure adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            _ = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "pet kitten" });
            var gameMoveResult = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "d" });
            Assert.IsTrue(gameMoveResult.RoomMessage.Contains("KITTEN"));
        }

        [Test]
        public void PlayTestNewGame()
        {
            // Enter the Game and reset it inside the game interface
            string gameInstance;
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            gameInstance = newadv.InstanceID;
            PlayAdventure adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            var gameMoveResult = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "newgame" });
            Assert.IsTrue(gameMoveResult.InstanceID != gameInstance);
        }


    }
}