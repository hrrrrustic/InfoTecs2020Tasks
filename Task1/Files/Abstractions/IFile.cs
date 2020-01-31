namespace Task1.Files.Abstractions
{
    public interface IFile
    {
        byte[] GetValue();
        bool IsOpenToRead();
    }
}