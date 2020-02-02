using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Task1
{
    public class AppConfig
    {
        public static Result<AppConfig> GetConfig()
        {
            if (File.Exists("AppConfig.json"))
                return DeserializeConfig();

            return new Result<AppConfig>(new FileNotFoundException());
        }

        private static Result<AppConfig> DeserializeConfig()
        {
            try
            {
                AppConfig config = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText("AppConfig.json"));
                return new Result<AppConfig>(config);
            }
            catch (Exception e)
            {
                return new Result<AppConfig>(e);
            }
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