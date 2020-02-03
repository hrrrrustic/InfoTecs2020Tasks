using System;
using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public interface IFileStorage : ISource
    {
        Result CreateFile(IFile file);
        Result<bool> FileExist(string fileName);
        Result<IEnumerable<IFile>> GetFiles();
        Result InitializeStorage();
        Result<IFileStorage> CreateInnerStorage(string storageName);
        Result Clone(IFileStorage destination); 
    }
}