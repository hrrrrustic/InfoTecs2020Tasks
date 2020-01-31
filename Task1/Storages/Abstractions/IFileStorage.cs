using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public interface IFileStorage
    {
        void CreateFile(BaseFile file);
        bool FileExist(string fileName);
        IEnumerable<BaseFile> GetFiles();
    }
}