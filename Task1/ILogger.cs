namespace Task1
{
    public interface ILogger
    {
        LoggingLevel MinimumLoggingLevel { get; }
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}