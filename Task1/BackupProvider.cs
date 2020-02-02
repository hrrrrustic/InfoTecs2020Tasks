using System;
using System.Collections.Generic;
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

        public void CreateBackup(IEnumerable<IFileStorage> sourceStorage, IFileStorage destinationStorage)
        {
            Result<bool> destinationIsAvailableResult = destinationStorage.IsAvailable();
            if (!destinationIsAvailableResult.Ok)
                _logger.Error("Destination storage is not available");

            if (!destinationIsAvailableResult.Value)
                destinationStorage.InitializeStorage();

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");
            Result<IFileStorage> createInnerStorageResult = destinationStorage.CreateInnerStorage(storageName);
            if (!createInnerStorageResult)
                _logger.Error("Creation backup folder is impossible");

            IFileStorage backupStorage = createInnerStorageResult.Value;

            foreach (IFileStorage filesStorage in sourceStorage) filesStorage.Clone(backupStorage);
        }
    }
}