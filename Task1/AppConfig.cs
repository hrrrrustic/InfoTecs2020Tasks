using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task1
{
    public class AppConfig
    {
        public static AppConfig GetConfig()
        {
            if(File.Exists("../../../AppConfig.json"))
                return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText("../../../AppConfig.json"));

            return new AppConfig("Debug", string.Empty, new List<string>(0));
        }

        public AppConfig(string loggerLevel, string destinationFolder, List<string> sourceFolders)
        {
            LoggerLevel = loggerLevel;
            DestinationFolder = destinationFolder;
            SourceFolders = sourceFolders;
        }
        public string LoggerLevel { get; }
        public string DestinationFolder { get; }
        public List<string> SourceFolders { get; }
    }
}