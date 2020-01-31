using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Files.Implementations;
using Task1.Storages.Abstractions;

namespace Task1.Storages.Implementations
{
    public class LocalStorage : BaseStorage
    {
        public LocalStorage(string connectionString) : base(connectionString)
        {
        }

        public override bool IsAvailable()
        {
            return Directory.Exists(ConnectionString);
        }

        public override void CreateFile(string filename)
        {
            throw new NotImplementedException();
        }

        public override void CreateFile(BaseFile file)
        {
            throw new NotImplementedException();
        }

        public override bool FileExist(string fileName)
        {
            throw new NotImplementedException();
        }

        public override BaseStorage CreateInnerStorage(string storageName)
        {
            string newStorageConnectionString = Path.Combine(ConnectionString, storageName);
            Directory.CreateDirectory(newStorageConnectionString);
            
            return new LocalStorage(newStorageConnectionString);
        }

        public override IEnumerable<BaseFile> GetFiles()
        {
            throw new NotImplementedException();
        }
    }
}