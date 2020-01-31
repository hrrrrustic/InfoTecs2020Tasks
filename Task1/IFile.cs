namespace Task1
{
    public interface IFile
    {
        void WriteToFile(string value);
        bool IsOpenToRead();
    }
}