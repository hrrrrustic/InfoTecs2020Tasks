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
        public static void CreateBackup(IEnumerable<IFileStorage> sourceStorage, IFileStorage destinationStorage)
        {
            if (!destinationStorage.IsAvailable()) 
                destinationStorage.InitializeStorage();

            string storageName = "Backup_" + DateTime.Now.ToString("hh-mm-ss_dd/MM/yyyy");
            IFileStorage backupStorage = destinationStorage.CreateInnerStorage(storageName);

            foreach (IFileStorage filesStorage in sourceStorage)
            {
                CopyStorage(filesStorage, backupStorage);
            }
        }

        private static void CopyStorage(IFileStorage sourceStorage, IFileStorage destination)
        {
            IEnumerable<IFile> files = sourceStorage.GetFiles();

            foreach (IFile file in files)
            {
                CopyFile(file, destination);
            }
        }

        private static void CopyFile(IFile file, IFileStorage destination)
        {
            if (destination.FileExist(file.Name))
                throw new Exception("3");

            if (!file.CanBeOpenedToRead())
                throw new Exception("4");

            destination.CreateFile(file);
        }
    }
}