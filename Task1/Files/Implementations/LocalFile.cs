using System;
using System.IO;
using Task1.Files.Abstractions;
using Task1.Logger;

namespace Task1.Files.Implementations
{
    public class LocalFile : IFile
    {
        public LocalFile(string path, string fileName)
        {
            SourcePath = path;
            Name = fileName;
        }

        public Result<bool> IsAvailable()
        {
            try
            {
                bool exists = File.Exists(SourcePath);
                return new Result<bool>(exists);
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in checking available");
                return new Result<bool>(e);
            }
        }

        public string Name { get; }

        public string SourcePath { get; }

        public Result<byte[]> GetValue()
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(SourcePath);
                return new Result<byte[]>(bytes);
            }
            catch (Exception e)
            {
                LoggerProvider.LoggerInstance.Error("Error in reading file");
                return new Result<byte[]>(e);
            }
        }

        public Result<bool> CanBeOpenedToRead()
        {
            Result<bool> isAvailableResult = IsAvailable();

            if (!isAvailableResult.HasValue())
                return isAvailableResult;

            if (!isAvailableResult.Value)
                return new Result<bool>(false);

            try
            {
                FileStream stream = File.Open(SourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Close();
                return new Result<bool>(true);
            }
            catch
            {
                LoggerProvider.LoggerInstance.Error("Error in checking file to read");
                return new Result<bool>(false);
            }
        }
    }
}