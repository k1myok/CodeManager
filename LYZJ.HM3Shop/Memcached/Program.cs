using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memcached
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] serverList = { "192.168.1.100:11211", "192.168.1.250:11211" };

            SockIOPool pool = SockIOPool.GetInstance("test");
            pool.SetServers(serverList);
            pool.Initialize();

            //实例化对象
            MemcachedClient mc = new MemcachedClient();
            mc.PoolName = "test";
            mc.EnableCompression = false;

            //调用方法
            //mc.Add("sds", "sds");
            mc.Set("foo", "sadf");
            //pool.Shutdown();

            Console.WriteLine("添加成功");
            Console.ReadKey();
        }
    }
}
