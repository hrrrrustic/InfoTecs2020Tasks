using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1
{
    public interface IFileStorage
    {
        void CreateFile(string fileName);
        bool FileExist(string fileName);
        IEnumerable<BaseFile> GetFiles();
    }
}