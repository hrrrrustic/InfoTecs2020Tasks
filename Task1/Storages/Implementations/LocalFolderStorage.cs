using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.Files.Implementations;
using Task1.Storages.Abstractions;
using File = Task1.Files.Abstractions.File;

namespace Task1.Storages.Implementations
{
    public class LocalFolderStorage : IFilesStorage
    {
        public LocalFolderStorage(string connectionString)
        {
            ConnectionString = connectionString;

            if (IsAvailable())
                Files = Directory.GetFiles(connectionString)
                    .Select(k => new LocalFile(Path.Combine(connectionString, k)));
            else
                Files = Array.Empty<File>();
        }

        public bool IsAvailable()
        {
            return Directory.Exists(ConnectionString);
        }

        public string ConnectionString { get; }
        public void Copy(string destination)
        {
            foreach (File file in Files)
            {
                file.Copy(destination);
            }
        }

        public IEnumerable<File> Files { get; }
    }
}