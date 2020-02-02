namespace Task1.Files.Abstractions
{
    public interface IFile : ISource
    {
        Result<byte[]> GetValue();
        Result<bool> CanBeOpenedToRead();
        string Name { get; }
    }
}