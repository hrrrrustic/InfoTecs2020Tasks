namespace Task1
{
    public interface ISource
    {
        Result<bool> IsAvailable();
        string Path { get; }
    }
}