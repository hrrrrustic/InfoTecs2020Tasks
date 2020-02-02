namespace Task1.Files.Abstractions
{
    public interface IFile : ISource
    {
        string Name { get; }
        Result<byte[]> GetValue();
        Result<bool> CanBeOpenedToRead();
    }
}