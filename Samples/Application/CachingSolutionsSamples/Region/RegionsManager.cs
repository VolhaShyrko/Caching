using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using NorthwindLibrary;

namespace CachingSolutionsSamples.Region
{
    public class RegionsManager
    {
        private readonly IRegionsCache _cache;

        public RegionsManager(IRegionsCache cache)
        {
            this._cache = cache;
        }

        public IEnumerable<NorthwindLibrary.Region> GetRegions()
        {
            Console.WriteLine("Get Regions");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var regions = this._cache.Get(user);

            if (regions == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    regions = dbContext.Regions.ToList();
                    this._cache.Set(user, regions);
                }
            }

            return regions;
        }
    }
}
