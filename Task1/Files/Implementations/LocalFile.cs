using System;
using System.IO;
using Task1.Files.Abstractions;

namespace Task1.Files.Implementations
{
    public class LocalFile : IFile
    {
        public LocalFile(string path, string fileName)
        {
            Path = path;
            Name = fileName;
        }

        public Result<bool> IsAvailable()
        {
            try
            {
                bool exists = File.Exists(Path);
                return new Result<bool>(exists);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        public string Name { get; }

        public string Path { get; }

        public Result<byte[]> GetValue()
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(Path);
                return new Result<byte[]>(bytes);
            }
            catch (Exception e)
            {
                return new Result<byte[]>(e);
            }
        }

        public Result<bool> CanBeOpenedToRead()
        {
            Result<bool> isAvailableResult = IsAvailable();

            if (!isAvailableResult)
                return isAvailableResult;

            if (!isAvailableResult.Value)
                return new Result<bool>(false);

            try
            {
                FileStream stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Close();
            }
            catch
            {
                return new Result<bool>(false);
            }

            return new Result<bool>(true);
        }
    }
}