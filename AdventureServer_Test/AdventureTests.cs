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
            var adv = new PlayAdventure();
            var getadv = _adventureFramework.NewGame(1);
            Assert.IsTrue(getadv.InstanceID.Length > 10);

        }





        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}