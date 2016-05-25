using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.Common
{
    public static class CacheHelper
    {
        //单例模式
        public static readonly Memcached.ClientLibrary.MemcachedClient mc;

        static CacheHelper()
        {
            string[] servers = { "192.168.1.100:11211" };

            //初始化池 
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;
        }


        public static  void Add(string key, object value, DateTime ExpireDate)
        {
            if (mc.KeyExists(key))
            {
                mc.Set(key, value, ExpireDate);
            }
            else
            {
                mc.Add(key, value, ExpireDate);
            }
        }

        public static object Set(string key)
        {
            return mc.Get(key);
        }

        public static void Add<T>(string key, T value, DateTime ExpireDate)
        {
            Add(key, value, ExpireDate);
        }

        public static T Get<T>(string key) where T : class
        {
            return mc.Get(key) as T;
        }
    }
}
