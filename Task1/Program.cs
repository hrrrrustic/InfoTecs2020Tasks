using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Test
    {
        public string LoggerLevel { get; set; } = "Debug";
        public string DestinationFolder { get; set; } = "Destination";
        public List<string> SourceFolders { get; set; } = new List<string>();
    }
}
