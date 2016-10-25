using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization;

using StackExchange.Redis;

namespace CachingSolutionsSamples.Region
{
    using System;

    class RegionsRedisCache : IRegionsCache
    {
        private ConnectionMultiplexer redisConnection;
        string prefix = "Cache_Regions";

        readonly DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<NorthwindLibrary.Region>));

        public RegionsRedisCache(string hostName)
		{
		    this.redisConnection = ConnectionMultiplexer.Connect(hostName);
		}

        public IEnumerable<NorthwindLibrary.Region> Get(string forUser)
        {
            var db = this.redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<NorthwindLibrary.Region>)this.serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<NorthwindLibrary.Region> regions)
        {
            var db = this.redisConnection.GetDatabase();
            var key = this.prefix + forUser;

            if (regions == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                this.serializer.WriteObject(stream, regions);
                db.StringSet(key, stream.ToArray(), TimeSpan.FromMinutes(5), When.Always, CommandFlags.None);
            }
        }
    }
}
