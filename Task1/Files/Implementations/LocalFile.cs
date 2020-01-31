using System.IO;
using Task1.Files.Abstractions;
using Task1.Storages.Abstractions;

namespace Task1.Files.Implementations
{
    public class LocalFile : BaseFile
    {
        public LocalFile(string connectionString, string filename) : base(connectionString, filename)
        {
        }

        public override bool IsAvailable()
        {
            return File.Exists(ConnectionString);
        }

        public override void Copy(BaseStorage destination)
        {
            string newFilePath = Path.Combine(destination.ConnectionString, "newName");
            File.Copy(ConnectionString, newFilePath);
        }

        public override bool CanBeCopied(BaseStorage destination)
        {
            return IsAvailable() && destination.IsAvailable();
        }
    }
}