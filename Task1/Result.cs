using System;

namespace Task1
{
    public class Result
    {
        public Exception ThrewException { get; }

        private Result(Exception threwException)
        {
            ThrewException = threwException;
        }
        private Result() : this(null)
        {
        }

        public static Result Ok()
        {
            return new Result();
        }
        public static Result OnError(Exception ex)
        {
            return new Result(ex);
        }
    }
    public class Result<T>
    {
        public Result(Exception threwException)
        {
            ThrewException = threwException;
            Value = default;
        }

        public Result(T value)
        {
            ThrewException = null;
            Value = value;
        }

        public bool HasValue() => ThrewException == null;
        public Exception ThrewException { get; }

        private T _value;
        public T Value
        {
            get => ThrewException != null ? throw ThrewException : _value;
            private set => _value = value;
        }

        public static Result<T> WrapSafeResult(Func<T> func)
        {
            try
            {
                T result = func.Invoke();
                return new Result<T>(result);
            }
            catch(Exception ex)
            {
                return new Result<T>(ex);
            }
        }
    }
}