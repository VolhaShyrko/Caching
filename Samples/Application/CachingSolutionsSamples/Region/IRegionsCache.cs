using System.Collections.Generic;

namespace CachingSolutionsSamples.Region
{
    public interface IRegionsCache
    {
        IEnumerable<NorthwindLibrary.Region> Get(string forUser);
        void Set(string forUser, IEnumerable<NorthwindLibrary.Region> regions);
    }
}
