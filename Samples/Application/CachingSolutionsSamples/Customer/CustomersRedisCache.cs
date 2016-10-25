using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

using StackExchange.Redis;

namespace CachingSolutionsSamples.Customer
{
    class CustomersRedisCache : ICustomersCache
    {
        private ConnectionMultiplexer redisConnection;
        string prefix = "Cache_Customers";

        readonly DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<NorthwindLibrary.Customer>));

        public CustomersRedisCache(string hostName)
        {
            this.redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<NorthwindLibrary.Customer> Get(string forUser)
        {
            var db = this.redisConnection.GetDatabase();
            byte[] s = db.StringGet(this.prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<NorthwindLibrary.Customer>)this.serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<NorthwindLibrary.Customer> customers)
        {
            var db = this.redisConnection.GetDatabase();
            var key = this.prefix + forUser;

            if (customers == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                this.serializer.WriteObject(stream, customers);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
