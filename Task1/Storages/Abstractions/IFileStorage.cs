using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public interface IFileStorage : ISource
    {
        void CreateFile(IFile file);
        Result<bool> FileExist(string fileName);
        Result<IEnumerable<IFile>> GetFiles();
        void InitializeStorage();
        Result<IFileStorage> CreateInnerStorage(string storageName);
        void Clone(IFileStorage destination);
    }
}