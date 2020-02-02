using System.Collections.Generic;

namespace Task1.Logger
{
    public class SimpleFileLogger : ILogger
    {
        public SimpleFileLogger(LoggingLevel minimumLoggingLevel)
        {
            MinimumLoggingLevel = minimumLoggingLevel;
        }

        public ICollection<string> Logs { get; } = new List<string>();

        public LoggingLevel MinimumLoggingLevel { get; }

        public void Info(string message)
        {
            Log(LoggingLevel.Info, message);
        }

        public void Debug(string message)
        {
            Log(LoggingLevel.Debug, message);
        }

        public void Error(string message)
        {
            Log(LoggingLevel.Error, message);
        }

        private void Log(LoggingLevel level, string message)
        {
            if (level < MinimumLoggingLevel)
                return;

            Logs.Add(level + " : " + message);
        }
    }
}