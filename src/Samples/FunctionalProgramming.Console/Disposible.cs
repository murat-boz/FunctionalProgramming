using System;

namespace FunctionalProgramming.Console
{
    public static class Disposible
    {
        public static TResult Using<TDisposible, TResult>(Func<TDisposible> factory, Func<TDisposible, TResult> func) where TDisposible : IDisposable
        {
            using (var disposible = factory())
            {
                return func(disposible);
            }
        }
    }
}
