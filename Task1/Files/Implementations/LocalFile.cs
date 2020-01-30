using System.IO;
using File = Task1.Files.Abstractions.File;

namespace Task1.Files.Implementations
{
    public class LocalFile : File
    {
        public LocalFile(string connectionString) : base(connectionString)
        {
        }

        public override bool IsAvailable()
        {
            return System.IO.File.Exists(ConnectionString);
        }

        public override void Copy(string destination)
        {
            string newFilePath = Path.Combine(destination, "newName");
            System.IO.File.Copy(ConnectionString, newFilePath);
        }
    }
}