using System;
using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public abstract class BaseStorage : IFileStorage
    {
        protected BaseStorage(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public abstract bool IsAvailable();
        public abstract void CreateFile(BaseFile file);
        public abstract bool FileExist(string fileName);
        public abstract BaseStorage CreateInnerStorage(string storageName);
        public abstract IEnumerable<BaseFile> GetFiles();

        public string ConnectionString { get; }
    }
}