using System;

namespace Task1.Logger
{
    public static class LoggerProvider
    {
        public static ILogger LoggerInstance { get; set; }

        static LoggerProvider()
        {
            LoggerInstance = new SimpleFileLogger(LoggingLevel.Debug);
        }
    }
}