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
        private MemoryCacheOptions mco = new MemoryCacheOptions();
        private IMemoryCache _gameCache;


        [SetUp]
        public void Setup()
        {
            _gameCache = new MemoryCache(mco);
            _welcomeSontroller = new WelcomeController();
            _adventureFramework = new AdventureFramework(_gameCache);

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


    }
}