using System;
using System.Collections.Generic;
using Task1.Logger;
using Task1.Storages.Abstractions;

namespace Task1
{
    public class BackupProvider
    {

        public Result CreateBackup(IEnumerable<IFileStorage> sourceStorage, IFileStorage destinationStorage)
        {
            LoggerProvider.LoggerInstance.Info("Checking destination folder");

            Result<bool> destinationIsAvailableResult = destinationStorage.IsAvailable();
            if (!destinationIsAvailableResult.HasValue())
            {
                LoggerProvider.LoggerInstance.Error("Error in checking destination storage availability");
                return Result.OnError(destinationIsAvailableResult.ThrewException);
            }

            LoggerProvider.LoggerInstance.Info("Destination folder available");

            if (!destinationIsAvailableResult.Value)
            {
                LoggerProvider.LoggerInstance.Info("Init destination storage");
                Result initStorageResult = destinationStorage.InitializeStorage();

                if (initStorageResult.IsException())
                {
                    LoggerProvider.LoggerInstance.Error("Error in initing destination storage");
                    return Result.OnError(initStorageResult.ThrewException);
                }

                LoggerProvider.LoggerInstance.Info("Destination folder inited");

            }

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");

            LoggerProvider.LoggerInstance.Info("Create inner backup storage");
            Result<IFileStorage> createInnerStorageResult = destinationStorage.CreateInnerStorage(storageName);

            if (!createInnerStorageResult.HasValue())
            {
                LoggerProvider.LoggerInstance.Error("Creation backup folder is impossible");
                return Result.OnError(createInnerStorageResult.ThrewException);
            }

            LoggerProvider.LoggerInstance.Info("Inner backup storage created");

            IFileStorage backupStorage = createInnerStorageResult.Value;

            LoggerProvider.LoggerInstance.Debug("Start copy files");
            foreach (IFileStorage filesStorage in sourceStorage) filesStorage.Clone(backupStorage);
            LoggerProvider.LoggerInstance.Debug("Files copied");
            return Result.Ok();
        }
    }
}