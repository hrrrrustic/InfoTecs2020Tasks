namespace Task1
{
    public interface ISource
    {
        string SourcePath { get; }
        Result<bool> IsAvailable();
    }
}