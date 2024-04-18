using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCacheRedis
{
    public class HelperCacheMultiplexer
    {
        private static Lazy<ConnectionMultiplexer>
            CreateConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string cacheRedisKeys = "cacheredisalejo.redis.cache.windows.net:6380,password=jgyKzMOcGrWh0wlGAobla2C5JDvT55e1wAzCaEX9NBc=,ssl=True,abortConnect=False";
                return ConnectionMultiplexer.Connect(cacheRedisKeys);
            });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return CreateConnection.Value;
            }
        }
    }
}
