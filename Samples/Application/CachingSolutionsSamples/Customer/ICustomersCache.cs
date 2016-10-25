using System.Collections.Generic;

namespace CachingSolutionsSamples.Customer
{
    public interface ICustomersCache
    {
        IEnumerable<NorthwindLibrary.Customer> Get(string forUser);
        void Set(string forUser, IEnumerable<NorthwindLibrary.Customer> customers);
    }
}
