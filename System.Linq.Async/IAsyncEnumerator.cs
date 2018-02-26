using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public interface IAsyncEnumerator<T> : IAsyncDisposable
    {
        T Current { get; }
        Task<bool> MoveNextAsync();
    }
}
