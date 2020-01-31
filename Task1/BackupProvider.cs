using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Storages.Abstractions;

namespace Task1
{
    public static class BackupProvider
    {
        public static void CreateBackup(IEnumerable<BaseStorage> sourceStorage, BaseStorage destinationStorage)
        {
            if (!destinationStorage.IsAvailable())
                throw new Exception();

            string storageName = "Backup_" + DateTime.Now.ToString("hh:mm:ss_dd/MM/yyyy");
            BaseStorage backupStorage = destinationStorage.CreateInnerStorage(storageName);

            foreach (BaseStorage filesStorage in sourceStorage)
            {
                CopyStorage(filesStorage, backupStorage);
            }
        }

        private static void CopyStorage(BaseStorage sourceStorage, BaseStorage destination)
        {
            IEnumerable<BaseFile> files = sourceStorage.GetFiles();

            foreach (BaseFile file in files)
            {
                CopyFile(file, destination);
            }
        }

        private static void CopyFile(BaseFile file, BaseStorage destination)
        {
            if (destination.FileExist(file.FileName))
                throw new Exception();

            if (!file.IsOpenToRead())
                throw new Exception();

            destination.CreateFile(file);
        }
    }
}