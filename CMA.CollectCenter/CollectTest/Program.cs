using CMA.CollectCenter.Base;
using CMA.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectTest
{
    class Program
    {
        private static Dictionary<string, CollectorInfo> _collectors;
        static void Main(string[] args)
        {
            _collectors = new Dictionary<string, CollectorInfo>();


            var web1Collector = CollectorFactory.CreateInstance("Collector1", new DBParamter() { DBName = "test", Host = "127.0.0.1", Password = "qwe123!@#", Port = 3306, Username = "root" });
            var web2Collector = CollectorFactory.CreateInstance("Collector1", new DBParamter() { DBName = "test", Host = "127.0.0.1", Password = "qwe123!@#", Port = 3306, Username = "root" });
            _collectors.Add("webCollector1", new CollectorInfo() { Interval = 10, Collector = webCollector, LastTime = DateTime.MinValue });


            while (_collectors.Count > 0)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        _collectors.Clear();
                        break;

                    default:
                        foreach (var key in _collectors.Keys)
                        {
                            _collectors[key].Collector.Collector();
                        }
                        break;
                }
            }


            Console.ReadKey();
        }

        private static void CollectorA(CollectorInfo aaa)
        {
            if ((DateTime.Now - aaa.LastTime).TotalSeconds > aaa.Interval)
            {
                aaa.Collector.Collector();
                aaa.LastTime = DateTime.Now;
            }
        }


        private void TestCollect()
        {

            var a1 = CollectorFactory.CreateInstance("Collector1,Collector2", new DBParamter() { Host = "zxm", Username = "sa", Password = "123" });
            _collectors.Add("a1", new CollectorInfo() { Interval = 10, Collector = a1, LastTime = DateTime.MinValue });

            var a2 = CollectorFactory.CreateInstance("Collector1", new DBParamter() { Host = "zxm", Username = "sa", Password = "123" });
            _collectors.Add("a2", new CollectorInfo() { Interval = 5, Collector = a1, LastTime = DateTime.MinValue });

            while (_collectors.Count > 0)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.C:
                        var aaa = _collectors["a1"];
                        CollectorA(aaa);
                        aaa = _collectors["a2"];
                        CollectorA(aaa);
                        break;
                    case ConsoleKey.Q:
                        _collectors.Remove("a1");
                        break;
                    case ConsoleKey.E:
                        var aaaa = _collectors["a1"];
                        aaaa.Collector.Collector();
                        aaaa = _collectors["a2"];
                        aaaa.Collector.Collector();
                        break;
                }
            }
        }
    }
}
