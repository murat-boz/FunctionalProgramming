using System;

namespace FunctionalProgramming
{
    public static class ResultExtensions
    {
        public static Result<T> Create<T>(this Result<T> result)
        {
            return result;
        }

        public static Result<T> Finally<T>(this Result<T> result, Action<T> action)
        {
            if (result.Error == Statement.Break)
            {
                return result;
            }

            action?.Invoke(result.Value);

            return result;
        }

        public static Result<T> IfThen<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            if (result.Error == Statement.Continue)
            {
                return result;
            }

            if (predicate(result.Value))
            {
                action?.Invoke(result.Value);
            }

            return result;
        }

        public static Result<T> IfThen<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action, Statement error = Statement.Default)
        {
            if (result.Error == Statement.Continue)
            {
                return result;
            }

            if (predicate(result.Value))
            {
                action?.Invoke(result.Value);
            }

            return error == Statement.Default
                ? result
                : Result<T>.Failure(result.Value, error);
        }

        public static Result<T> IfThenContinue<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            if (result.Error == Statement.Continue)
            {
                return result;
            }

            if (predicate(result.Value))
            {
                action?.Invoke(result.Value);

                return result;
            }
            else
            {
                return Result<T>.Failure(result.Value, Statement.Continue);
            }
        }

        public static Result<T> IfThenBreak<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            if (result.Error == Statement.Continue)
            {
                return result;
            }

            if (predicate(result.Value))
            {
                action?.Invoke(result.Value);

                return result;
            }
            else
            {
                return Result<T>.Failure(result.Value, Statement.Break);
            }
        }
    }
}
