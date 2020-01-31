namespace Task1
{
    public class SimpleFileLogger : ILogger
    {
        public SimpleFileLogger(LoggingLevel currentMinimumLevel)
        {
            CurrentMinimumLevel = currentMinimumLevel;
        }

        public LoggingLevel CurrentMinimumLevel { get; }

        public void Info()
        {
            throw new System.NotImplementedException();
        }

        public void Debug()
        {
            throw new System.NotImplementedException();
        }

        public void Error()
        {
            throw new System.NotImplementedException();
        }
    }
}