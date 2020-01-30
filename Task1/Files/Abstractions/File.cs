using System.IO;

namespace Task1.Files.Abstractions
{
    public abstract class File : IAvailableSource, ICopyable
    {
        protected File(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public abstract bool IsAvailable();

        public string ConnectionString { get; }
        public abstract void Copy(string destination);
    }
}