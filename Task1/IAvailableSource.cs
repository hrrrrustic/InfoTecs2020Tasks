namespace Task1
{
    public interface IAvailableSource
    {
        bool IsAvailable();
        string ConnectionString { get; }
    }
}