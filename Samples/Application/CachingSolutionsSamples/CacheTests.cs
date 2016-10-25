using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
    using CachingSolutionsSamples.Customer;
    using CachingSolutionsSamples.Region;

    [TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

        [TestMethod]
        public void RegionsMemoryCache()
        {
            var manager = new RegionsManager(new RegionsCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(manager.GetRegions().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RegionsRedisCache()
        {
            var manager = new RegionsManager(new RegionsRedisCache("localhost,abortConnect=false,syncTimeout=3000"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(manager.GetRegions().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CustomersMemoryCache()
        {
            var manager = new CustomersManager(new CustomersCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(manager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CustomersRedisCache()
        {
            var manager = new CustomersManager(new CustomersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(manager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }
	}
}
