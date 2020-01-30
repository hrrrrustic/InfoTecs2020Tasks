using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Storages.Abstractions;

namespace Task1
{
    public class BackupProvider
    {
        public static void CreateBackup(IFilesStorage sourceStorage, IFilesStorage destinationStorage)
        {
            sourceStorage.Copy(destinationStorage.ConnectionString);
        }
    }
}