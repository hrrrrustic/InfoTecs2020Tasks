using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Bench));
        }
    }
    public class Bench
    {
        private const string PathToHabrLocalFile =
            @"F:\VSProjects\Infotecs2020Tasks\ConsoleApp1\bin\Release\netcoreapp3.1\test.txt";
        private const string PathToLentaLocalFile =
            @"F:\VSProjects\Infotecs2020Tasks\ConsoleApp1\bin\Release\netcoreapp3.1\test2.txt";

        private const string PathToLentaHttp = "https://lenta.ru/rss/news";
        private const string PathToHabrHttp = "https://habr.com/ru/rss/all/";


        [Benchmark]
        public void ParseOneFromLocal()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToHabrLocalFile);
        }

        [Benchmark]
        public void ParseTwoFromLocal()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToHabrLocalFile);
            doc.Load(PathToLentaLocalFile);

        }

        [Benchmark]
        public void ParseOneFromHttp()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToHabrHttp);
        }

        [Benchmark]
        public void ParseTwoFromHttp()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToHabrHttp);
            doc.Load(PathToLentaHttp);

        }
    }
}
