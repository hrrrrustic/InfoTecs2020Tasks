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

        public override void CreateFile(BaseFile file)
        {
            //TODO: как на счет обернуть в трайкетч и с нормальной ошибкой хендлить?
            using FileStream stream = File.Create(Path.Combine(ConnectionString, file.FileName));

            stream.Write(file.GetValue());
        }

        public override bool FileExist(string fileName)
        {
            return File.Exists(Path.Combine(ConnectionString, fileName));
        }

        public override BaseStorage CreateInnerStorage(string storageName)
        {
            string newStorageConnectionString = Path.Combine(ConnectionString, storageName);
            //TODO: А ты хендлишь то, что не существует этого стораджа?
            Directory.CreateDirectory(newStorageConnectionString);
            
            return new LocalStorage(newStorageConnectionString);
        }

        public override void InitializeStorage()
        {
            Directory.CreateDirectory(ConnectionString);
        }

        public override IEnumerable<BaseFile> GetFiles()
        {
            //TODO?
            if (!IsAvailable())
                throw new Exception("1");

            return Directory.GetFiles(ConnectionString).Select(k =>
            {
                string fileName = Path.GetFileName(k);
                return new LocalFile(k, fileName);
            });
        }
    }
}