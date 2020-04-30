using Javirs.Common.Caching;
using System;
using Winner.Framework.Utils;

namespace Winner.OAuth.Facade
{
    public class CacheHelper
    {
        private static object lockObj = new object();
        private static ICache Caching;
        public static ICache Default
        {
            get
            {
                if (Caching == null)
                {
                    lock (lockObj)
                    {
                        if (Caching == null)
                        {
                            bool disableRedis = ConfigProvider.GetAppSetting<bool>("disableRedis", false);
                            string redis = ConfigurationManager.ConfigurationProvider.GetString("redis-server");
                            if (!disableRedis && !string.IsNullOrEmpty(redis))
                            {
                                Caching = new RedisCache(redis);
                            }
                            else
                            {
                                Caching = new MemoryCache();
                            }
                        }
                    }
                }
                return Caching;
            }
        }
    }
}
