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
            Result<bool> destinationIsAvailableResult = destinationStorage.IsAvailable();
            if (!destinationIsAvailableResult.Ok)
                return null;

            if (!destinationIsAvailableResult.Value) 
                destinationStorage.InitializeStorage();

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");
            Result<IFileStorage> createInnerStorageResult = destinationStorage.CreateInnerStorage(storageName);
            if (!createInnerStorageResult)
                return null;

            IFileStorage backupStorage = createInnerStorageResult.Value;

            foreach (IFileStorage filesStorage in sourceStorage)
            {
                filesStorage.Clone(backupStorage);
            }

            return _logger.Logs;
        }
    }
}