using System;

namespace Task1
{
    public class Result
    {
        public bool Ok { get; }
        public Exception ThrewException { get; }

        public Result(Exception threwException)
        {
            ThrewException = threwException;
            Ok = threwException == null;
        }
    }
}