namespace Task1.Files.Abstractions
{
    public interface IFile : ISource
    {
        byte[] GetValue();
        bool CanBeOpenedToRead();
        string Name { get; }
    }
}