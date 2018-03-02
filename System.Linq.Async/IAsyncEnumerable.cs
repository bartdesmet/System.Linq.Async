using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    [AsyncMethodBuilder(typeof(AsyncEnumerableTaskMethodBuilder<>))]
    public interface IAsyncEnumerable<out T>
    {
        IAsyncEnumerator<T> GetAsyncEnumerator();
    }
}
