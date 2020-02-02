using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Files.Implementations;
using Task1.Storages.Abstractions;

namespace Task1.Storages.Implementations
{
    public class LocalStorage : IFileStorage
    {
        public LocalStorage(string path)
        {
            Path = path;
        }

        public bool IsAvailable()
        {
            return Directory.Exists(Path);
        }

        public string Path { get; }
        public void CreateFile(IFile file)
        {
            if (FileExist(file.Name))
                throw new Exception("3");

            if (!file.CanBeOpenedToRead())
                throw new Exception("4");


            using FileStream stream = File.Create(System.IO.Path.Combine(Path, file.Name));

            stream.Write(file.GetValue());
        }

        public bool FileExist(string fileName)
        {
            return File.Exists(System.IO.Path.Combine(Path, fileName));
        }

        public IFileStorage CreateInnerStorage(string storageName)
        {
            string newStorageConnectionString = System.IO.Path.Combine(Path, storageName);
            Directory.CreateDirectory(newStorageConnectionString);
            
            return new LocalStorage(newStorageConnectionString);
        }

        public void Clone(IFileStorage destination)
        {
            IEnumerable<IFile> files = GetFiles();

            foreach (IFile file in files)
            {
                destination.CreateFile(file);
            }
        }

        public void InitializeStorage()
        {
            Directory.CreateDirectory(Path);
        }

        public IEnumerable<IFile> GetFiles()
        {
            if (!IsAvailable())
                throw new Exception("1");

            return Directory.GetFiles(Path).Select(k =>
            {
                string fileName = System.IO.Path.GetFileName(k);
                return new LocalFile(k, fileName);
            });
        }
    }
}