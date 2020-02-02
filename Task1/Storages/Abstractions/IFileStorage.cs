using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public interface IFileStorage : ISource
    {
        void CreateFile(IFile file);
        bool FileExist(string fileName);
        IEnumerable<IFile> GetFiles();
        void InitializeStorage();
        IFileStorage CreateInnerStorage(string storageName);
        void Clone(IFileStorage destination);

    }
}