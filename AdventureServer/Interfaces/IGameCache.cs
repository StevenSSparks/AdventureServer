using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureServer.Interfaces
{
    public interface IGameCache
    {
        public void CacheAdd(string key, Object T, int expireMins);

        public void CacheReplace(string key, Object T, int expireMins);

        public Object CacheGet(string key);

        public void CacheRemove(string key);
    }
}
