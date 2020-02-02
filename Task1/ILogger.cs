using System.Collections.Generic;

namespace Task1
{
    public interface ILogger
    {
        ICollection<string> Logs { get; }
        LoggingLevel MinimumLoggingLevel { get; }
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}