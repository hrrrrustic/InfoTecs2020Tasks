using System.IO;
using Task1.Storages.Abstractions;

namespace Task1.Files.Abstractions
{
    public abstract class BaseFile : IFile
    {
        protected BaseFile(string connectionString, string fileName)
        {
            ConnectionString = connectionString;
            FileName = fileName;
        }

        public abstract bool IsAvailable();

        public string ConnectionString { get; }
        public string FileName { get; }
        public abstract byte[] GetValue();
        public abstract bool IsOpenToRead();
    }
}