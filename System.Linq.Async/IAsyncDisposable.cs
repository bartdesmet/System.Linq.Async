using System.Threading.Tasks;

namespace System
{
    public interface IAsyncDisposable
    {
        Task DisposeAsync();
    }
}
