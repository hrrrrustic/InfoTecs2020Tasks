using System.Collections.Generic;
using Task1.Files.Abstractions;

namespace Task1.Storages.Abstractions
{
    public interface IFilesStorage : IAvailableSource, ICopyable
    {
        IEnumerable<File> Files { get; }
    }
}