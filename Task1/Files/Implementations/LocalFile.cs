using System;
using System.IO;
using Task1.Files.Abstractions;
using Task1.Storages.Abstractions;

namespace Task1.Files.Implementations
{
    public class LocalFile : IFile
    {
        public LocalFile(string path, string fileName)
        {
            Path = path;
            Name = fileName;
        }

        public bool IsAvailable()
        {
            return File.Exists(Path);
        }

        public string Name { get; }    

        public string Path { get; }

        public byte[] GetValue()
        {
            return File.ReadAllBytes(Path);
        }

        public bool CanBeOpenedToRead()
        {
            if (!IsAvailable())
                return false;

            try
            {
                FileStream stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
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