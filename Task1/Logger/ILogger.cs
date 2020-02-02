namespace Task1.Logger
{
    public interface ILogger
    {
        LoggingLevel MinimumLoggingLevel { get; }
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}