using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples.Region
{
    using System;

    internal class RegionsCache : IRegionsCache
    {
        readonly ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Regions";

        public IEnumerable<NorthwindLibrary.Region> Get(string forUser)
        {
            return (IEnumerable<NorthwindLibrary.Region>)this.cache.Get(this.prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<NorthwindLibrary.Region> regions)
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);

            //policy.ChangeMonitors.Add(new SqlChangeMonitor());
            //Volha: SqlChangeMonitor uses SQL query but the idea of Entity Framework is to get rid of it

            this.cache.Set(this.prefix + forUser, regions, policy);
        }
    }
}
