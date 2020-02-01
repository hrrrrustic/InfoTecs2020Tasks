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
        //TODO: кажется, это IsCanBeOpenedToRead 
        //ркмарка: мой секретарь говорит, что я не прав, мб это ок
        public abstract bool IsOpenToRead();
    }
}