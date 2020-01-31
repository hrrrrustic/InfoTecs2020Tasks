using System.IO;
using Task1.Storages.Abstractions;

namespace Task1.Files.Abstractions
{
    public abstract class BaseFile : IAvailableSource, IFile
    {
        protected BaseFile(string connectionString, string fileName)
        {
            ConnectionString = connectionString;
            FileName = fileName;
        }

        public abstract bool IsAvailable();

        public string ConnectionString { get; }
        public string FileName { get; }
        public abstract void Copy(BaseStorage destination);
        public abstract bool CanBeCopied(BaseStorage destination);
        public void WriteToFile(string value)
        {
            throw new System.NotImplementedException();
        }

        public bool IsOpenToRead()
        {
            throw new System.NotImplementedException();
        }
    }
}