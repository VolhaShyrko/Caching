using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples.Customer
{
    internal class CustomersCache : ICustomersCache
    {
        readonly ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Customers";

        public IEnumerable<NorthwindLibrary.Customer> Get(string forUser)
        {
            return (IEnumerable<NorthwindLibrary.Customer>)this.cache.Get(this.prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<NorthwindLibrary.Customer> customers)
        {
            this.cache.Set(this.prefix + forUser, customers, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
