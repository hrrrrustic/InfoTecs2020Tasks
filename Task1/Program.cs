using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Logger;
using Task1.Storages.Implementations;

namespace Task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            Result<AppConfig> result = AppConfig.GetConfig();
            if (!result.Ok)
                return;

            AppConfig config = result.Value;

            if (!Enum.TryParse(config.LoggerLevel, true, out LoggingLevel curLevel))
                curLevel = LoggingLevel.Debug;

            ILogger logger = new SimpleFileLogger(curLevel);

            LocalStorage destination = new LocalStorage(config.DestinationFolder);
            List<LocalStorage> sourceFolders = config.SourceFolders.Select(k => new LocalStorage(k)).ToList();

            new BackupProvider(logger).CreateBackup(sourceFolders, destination);
        }
    }
}