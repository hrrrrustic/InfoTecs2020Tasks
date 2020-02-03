using System;
using System.Collections.Generic;
using Task1.Logger;
using Task1.Storages.Abstractions;

namespace Task1
{
    public class BackupProvider
    {

        public void CreateBackup(IEnumerable<IFileStorage> sourceStorage, IFileStorage destinationStorage)
        {
            LoggerProvider.LoggerInstance.Info("Checking destination folder");

            Result<bool> destinationIsAvailableResult = destinationStorage.IsAvailable();
            if (!destinationIsAvailableResult.HasValue())
                LoggerProvider.LoggerInstance.Error("Destination storage is not available");

            if (!destinationIsAvailableResult.Value)
            {
                LoggerProvider.LoggerInstance.Info("init destination storage");
                destinationStorage.InitializeStorage();
            }

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");

            LoggerProvider.LoggerInstance.Info("Create inner storage");
            Result<IFileStorage> createInnerStorageResult = destinationStorage.CreateInnerStorage(storageName);

            if (!createInnerStorageResult.HasValue())
                LoggerProvider.LoggerInstance.Error("Creation backup folder is impossible");

            IFileStorage backupStorage = createInnerStorageResult.Value;

            LoggerProvider.LoggerInstance.Debug("Start copy files");
            foreach (IFileStorage filesStorage in sourceStorage) filesStorage.Clone(backupStorage);
        }
    }
}