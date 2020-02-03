using System.Collections.Generic;
using System.IO;

namespace Task1.Logger
{
    public class SimpleFileLogger : ILogger
    {
        public SimpleFileLogger(LoggingLevel minimumLoggingLevel)
        {
            MinimumLoggingLevel = minimumLoggingLevel;
        }

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
                File.AppendAllText("Log.txt",$"Loglevel : {level}. Message : {message}" + "\n");
        }
    }
}