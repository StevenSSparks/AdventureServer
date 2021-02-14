using NUnit.Framework;
using AdventureServer;
using AdventureServer.Controllers;
using AdventureServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureServer_Test
{
    public class AdventureTests
    {

        private WelcomeController _welcomeSontroller;
        private AdventureFramework _adventureFramework;
        private AdventureController _adventureController;
        private PlayAdventureController _playAdventureController;
        private MemoryCacheOptions mco = new MemoryCacheOptions();
        private IMemoryCache _gameCache;


        [SetUp]
        public void Setup()
        {
            _gameCache = new MemoryCache(mco);
            _welcomeSontroller = new WelcomeController();
            _adventureFramework = new AdventureFramework(_gameCache);
            _adventureController = new AdventureController(_adventureFramework);
            _playAdventureController = new PlayAdventureController(_adventureController);

        }

        [Test]
        public void SmokeTest()
        {
            var OkResult = _welcomeSontroller.Index();
            Assert.IsNotNull(OkResult);
        }

        [Test]
        public void TestCreateNewGame()
        {
           
            var getadv = _adventureFramework.ControllerEntry_NewGame(1);
            Assert.IsTrue(getadv.InstanceID.Length > 10);

        }

        [Test]
        public void TestItemMovemementAndCache()
        {
            var adv = new PlayAdventure();
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            adv.Items = _adventureFramework.Object_MoveItem(adv.Items, "bugle", 9999);
            _adventureFramework.GameInstance_Update(adv);
            adv = _adventureFramework.GameInstance_GetObject(adv.InstanceID);
            var itemloc = adv.Items.Find(i => i.Name.ToLower() == "bugle").Location;
            Assert.IsTrue(itemloc == 9999);


        }

        [Test]
        public void PlayAdventureTest()
        {
            var adv = new PlayAdventure();
            var newadv = _adventureFramework.ControllerEntry_NewGame(1);
            adv = _adventureFramework.GameInstance_GetObject(newadv.InstanceID);
            var test = _adventureController.GameMove(new GameMove { InstanceID = adv.InstanceID, Move = "get bugle" });
            Assert.IsTrue(true);

        }



    }
}