using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

using NorthwindLibrary;

namespace CachingSolutionsSamples.Customer
{
    class CustomersManager
    {
        private readonly ICustomersCache _cache;

        public CustomersManager(ICustomersCache cache)
        {
            this._cache = cache;
        }

        public IEnumerable<NorthwindLibrary.Customer> GetCustomers()
        {
            Console.WriteLine("Get Customers");

            var user = Thread.CurrentPrincipal.Identity.Name;
            var customers = this._cache.Get(user);

            if (customers == null)
            {
                Console.WriteLine("From DB");

                using (var dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    customers = dbContext.Customers.ToList();
                    this._cache.Set(user, customers);
                }
            }

            return customers;
        }
    }
}
