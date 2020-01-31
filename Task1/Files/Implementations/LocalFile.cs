using System;
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

        public override byte[] GetValue()
        {
            return File.ReadAllBytes(ConnectionString);
        }

        public override bool IsOpenToRead()
        {
            if (!IsAvailable())
                return false;

            try
            {
                FileStream stream = File.Open(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.Read);
                stream.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}