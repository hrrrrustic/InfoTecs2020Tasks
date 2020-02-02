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

        public Result<bool> IsAvailable()
        {
            try
            {
                bool exists = Directory.Exists(Path);
                return new Result<bool>(null, exists);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        public string Path { get; }
        public void CreateFile(IFile file)
        {
            if (FileExist(file.Name))
                throw new Exception("3");

            if (!file.CanBeOpenedToRead())
                throw new Exception("4");


            using FileStream stream = File.Create(System.IO.Path.Combine(Path, file.Name));

            Result<byte[]> readFileResult = file.GetValue();
            if(!readFileResult)
                throw new Exception();

            stream.Write(readFileResult.Value);
        }

        public Result<bool> FileExist(string fileName)
        {
            try
            {
                bool exists = File.Exists(System.IO.Path.Combine(Path, fileName));
                return new Result<bool>(exists);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        public Result<IFileStorage> CreateInnerStorage(string storageName)
        {
            try
            {
                string newStorageConnectionString = System.IO.Path.Combine(Path, storageName);
                Directory.CreateDirectory(newStorageConnectionString);

                return new Result<IFileStorage>(new LocalStorage(newStorageConnectionString));
            }
            catch (Exception e)
            {
                return new Result<IFileStorage>(e);
            }
        }

        public void Clone(IFileStorage destination)
        {
            Result<IEnumerable<IFile>> filesResult = GetFiles();

            if(!filesResult)
                throw new Exception();

            foreach (IFile file in filesResult.Value)
            {
                destination.CreateFile(file);
            }
        }

        public void InitializeStorage()
        {
            Directory.CreateDirectory(Path);
        }

        public Result<IEnumerable<IFile>> GetFiles()
        {
            if (!IsAvailable())
                throw new Exception("1");

            try
            {
                IEnumerable<LocalFile> files = Directory.GetFiles(Path).Select(k =>
                {
                    string fileName = System.IO.Path.GetFileName(k);
                    return new LocalFile(k, fileName);
                });

                return new Result<IEnumerable<IFile>>(files);
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<IFile>>(e);
            }
        }
    }
}