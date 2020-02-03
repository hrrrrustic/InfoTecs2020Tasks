using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Task1.Files.Abstractions;
using Task1.Files.Implementations;
using Task1.Logger;
using Task1.Storages.Abstractions;

namespace Task1.Storages.Implementations
{
    public class LocalStorage : IFileStorage
    {
        public LocalStorage(string path)
        {
            SourcePath = path;
        }

        public Result<bool> IsAvailable()
        {
            try
            {
                bool exists = Directory.Exists(SourcePath);
                return new Result<bool>(exists);
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in checking available");
                return new Result<bool>(e);
            }
        }

        public string SourcePath { get; }

        public Result CreateFile(IFile file)
        {
            try
            {
                if (FileExist(file.Name).HasValue())
                {
                    LoggerProvider.LoggerInstance.Error($"Duplicate file - {file.Name}");
                    return Result.OnError(new DuplicateNameException());
                }

                Result<bool> openFileResult = file.CanBeOpenedToRead();

                if (openFileResult.HasValue() && !openFileResult.Value)
                {
                    LoggerProvider.LoggerInstance.Error($"Can't open file to read - {file.Name}");
                    return Result.OnError(new MemberAccessException());
                }

                using FileStream stream = File.Create(Path.Combine(SourcePath, file.Name));

                Result<byte[]> readFileResult = file.GetValue();
                if (!readFileResult.HasValue())
                {
                    LoggerProvider.LoggerInstance.Error($"Can't read file - {file.Name}");
                    return Result.OnError(new DataException());
                }

                stream.Write(readFileResult.Value);
                LoggerProvider.LoggerInstance.Debug("File created");

                return Result.Ok();
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in creating file");
                return Result.OnError(e);
            }
        }

        public Result<bool> FileExist(string fileName)
        {
            try
            {
                bool exists = File.Exists(Path.Combine(SourcePath, fileName));
                return new Result<bool>(exists);
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in checking file existing");
                return new Result<bool>(e);
            }
        }

        public Result<IFileStorage> CreateInnerStorage(string storageName)
        {
            try
            {
                string newStorageConnectionString = Path.Combine(SourcePath, storageName);
                Directory.CreateDirectory(newStorageConnectionString);

                LoggerProvider.LoggerInstance.Info("Inner storage created");

                return new Result<IFileStorage>(new LocalStorage(newStorageConnectionString));
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in creating inner storage");
                return new Result<IFileStorage>(e);
            }
        }

        public Result Clone(IFileStorage destination)
        {
            try
            {
                LoggerProvider.LoggerInstance.Debug("Get files");
                Result<IEnumerable<IFile>> filesResult = GetFiles();

                if (!filesResult.HasValue())
                {
                    LoggerProvider.LoggerInstance.Error("Can't get files in storage");
                    return Result.OnError(new MemberAccessException());
                }

                LoggerProvider.LoggerInstance.Info("Copy files");
                foreach (IFile file in filesResult.Value) destination.CreateFile(file);

                return Result.Ok();
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in coping storage");
                return Result.OnError(e);
            }
        }

        public Result InitializeStorage()
        {
            try
            {
                Directory.CreateDirectory(SourcePath);
                LoggerProvider.LoggerInstance.Info("Storage inited");
                return Result.Ok();
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in init storage");
                return Result.OnError(e);
            }
        }

        public Result<IEnumerable<IFile>> GetFiles()
        {
            if (!IsAvailable().HasValue())
            {
                return new Result<IEnumerable<IFile>>(new DataException());
            }

            try
            {
                IEnumerable<LocalFile> files = Directory.GetFiles(SourcePath).Select(k =>
                {
                    string fileName = Path.GetFileName(k);
                    return new LocalFile(k, fileName);
                });

                return new Result<IEnumerable<IFile>>(files);
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in getting files");

                return new Result<IEnumerable<IFile>>(e);
            }
        }
    }
}