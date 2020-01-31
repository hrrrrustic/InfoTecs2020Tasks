namespace Task1
{
    public interface ILogger
    {
        LoggingLevel CurrentMinimumLevel { get; }
        void Info();
        void Debug();
        void Error();
    }
}