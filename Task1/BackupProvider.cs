using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Storages.Abstractions;

namespace Task1
{
    public class BackupProvider
    {
        private readonly ILogger _logger;
        public BackupProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<string> CreateBackup(IEnumerable<IFileStorage> sourceStorage, IFileStorage destinationStorage)
        {
            if (!destinationStorage.IsAvailable()) 
                destinationStorage.InitializeStorage();

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");
            IFileStorage backupStorage = destinationStorage.CreateInnerStorage(storageName);

            foreach (IFileStorage filesStorage in sourceStorage)
            {
                filesStorage.Clone(backupStorage);
            }

            return _logger.Logs;
        }
    }
}