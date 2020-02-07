using System.IO;

namespace Task1.Files.Abstractions
{
    public interface IFile : ISource
    {
        string Name { get; }
        Result<bool> TryOpenToRead(out Stream stream);
    }
}