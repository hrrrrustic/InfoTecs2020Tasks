using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Task1.Storages.Abstractions;
using Task1.Storages.Implementations;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            AppConfig config = AppConfig.GetConfig();

            if (!Enum.TryParse(config.LoggerLevel, true, out LoggingLevel curLevel))
                curLevel = LoggingLevel.Debug;

            ILogger logger = new SimpleFileLogger(curLevel);
            logger.Debug("Test");
            LocalStorage destination = new LocalStorage(config.DestinationFolder);
            List<LocalStorage> sourceFolders = config.SourceFolders.Select(k => new LocalStorage(k)).ToList();
            BackupProvider.CreateBackup(sourceFolders, destination);
        }
    }
}
