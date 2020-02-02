using System;

namespace Task1
{
    public class Result<T>
    {
        public Result(Exception threwException, T value)
        {
            ThrewException = threwException;
            Value = threwException == null ? default : value;
            Ok = threwException == null;
        }

        public Result(Exception threwException)
        {
            ThrewException = threwException;
            Value = default;
            Ok = false;
        }

        public Result(T value)
        {
            ThrewException = null;
            Value = value;
            Ok = true;
        }

        public bool Ok { get; }
        public Exception ThrewException { get; }
        public T Value { get; }

        public static implicit operator bool(Result<T> result)
        {
            return result.Ok;
        }
    }
}