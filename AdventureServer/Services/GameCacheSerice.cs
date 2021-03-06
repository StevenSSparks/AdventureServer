using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureServer.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureServer.Services.GameCacheService
{
    public class GameCacheService : IGameCache
    {
        public IMemoryCache _gameCache;

        public GameCacheService(IMemoryCache memoryCache)
        {
            _gameCache = memoryCache;
        }


        public void CacheAdd(string key, Object T, int expireMins)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
              .SetSlidingExpiration(TimeSpan.FromMinutes(expireMins));
            _ = _gameCache.Set(key, T, cacheEntryOptions);

        }

        public void CacheReplace(string key, Object T, int expireMins)
        {
            _gameCache.Remove(key);
            CacheAdd(key, T, expireMins);
        }

        public Object CacheGet(string key) => _gameCache.Get(key);

        public void CacheRemove(string key) => _gameCache.Remove(key);

    }
}
