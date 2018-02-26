using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    [AsyncMethodBuilder(typeof(AsyncEnumerableTaskMethodBuilder<>))]
    public interface IAsyncEnumerable<T>
    {
        IAsyncEnumerator<T> GetAsyncEnumerator();
    }
}
