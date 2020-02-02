namespace Task1
{
    public interface ISource
    {
        string Path { get; }
        Result<bool> IsAvailable();
    }
}