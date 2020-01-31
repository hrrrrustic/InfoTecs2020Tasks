namespace Task1.Files.Abstractions
{
    public interface IFile : IAvailableSource
    {
        byte[] GetValue();
        bool IsOpenToRead();
    }
}