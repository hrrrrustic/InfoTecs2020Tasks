using System;
using System.Collections.Generic;
using System.IO;
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

        private static void Run()
        {
            Result<AppConfig> result = AppConfig.GetConfig();
            if (!result.HasValue())
            {
                File.WriteAllText("Log.txt", "Config file not found \n");
                return;
            }

            AppConfig config = result.Value;

            if (!Enum.TryParse(config.LoggerLevel, true, out LoggingLevel curLevel))
                curLevel = LoggingLevel.Debug;

            ILogger logger = new SimpleFileLogger(curLevel);
            LoggerProvider.LoggerInstance = logger;

            LocalStorage destination = new LocalStorage(config.DestinationFolder);
            List<LocalStorage> sourceFolders = config.SourceFolders.Select(k => new LocalStorage(k)).ToList();

            LoggerProvider.LoggerInstance.Info("Starting backup");
            Result backupResult = new BackupProvider().CreateBackup(sourceFolders, destination);

            if (backupResult.IsException())
                LoggerProvider.LoggerInstance.Error($"Critical error : {backupResult.ThrewException.Message}");
            else
                LoggerProvider.LoggerInstance.Info("Backup created");
        }
    }
}