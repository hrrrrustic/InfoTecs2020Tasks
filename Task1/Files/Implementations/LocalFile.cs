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
                LoggerProvider.LoggerInstance.Debug($"Checking availability {SourcePath}");

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

        public Result<bool> TryOpenToRead(out Stream stream)
        {
            LoggerProvider.LoggerInstance.Debug("Trying open file to read");

            stream = null;

            Result<bool> isAvailableResult = IsAvailable();

            if (!isAvailableResult.HasValue())
            {
                LoggerProvider.LoggerInstance.Error("Error in opening file to read");
                return isAvailableResult;
            }

            if (!isAvailableResult.Value)
            {
                LoggerProvider.LoggerInstance.Error("Error in opening file to read");
                return new Result<bool>(false);
            }

            try
            {
                FileStream fileStream = File.Open(SourcePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream = fileStream;
                return new Result<bool>(true);
            }
            catch
            {
                LoggerProvider.LoggerInstance.Error("Error in opening file to read");
                return new Result<bool>(false);
            }
        }
    }
}