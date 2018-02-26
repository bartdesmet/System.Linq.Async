using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    internal static class Stuff
    {
        public static IVeryEnumerable<T> ConfigureAwait<T>(this IAsyncEnumerable<T> source, bool b)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IVeryEnumerable<T> : IEnumerable<T>, IAsyncEnumerable<T> { }
}

namespace System.Runtime.CompilerServices
{
    public sealed class AsyncEnumerableTaskMethodBuilder<T>
    {
        public IAsyncEnumerable<T> Task => throw new NotImplementedException();
    }
}
