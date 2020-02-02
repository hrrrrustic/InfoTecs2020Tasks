namespace Task1
{
    public interface ISource
    {
        bool IsAvailable();
        string Path { get; }
    }
}