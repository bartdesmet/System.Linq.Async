using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    internal static class OrderedAsyncEnumerable
    {
        public static IOrderedAsyncEnumerable<TSource> Create<TSource, TKey>(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending) => new OrderedAsyncEnumerableWithSyncSelector<TSource, TKey>(source, keySelector, comparer, descending);
        public static IOrderedAsyncEnumerable<TSource> Create<TSource, TKey>(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending) => new OrderedAsyncEnumerableWithAsyncSelector<TSource, TKey>(source, keySelector, comparer, descending);
    }

    internal sealed class OrderedAsyncEnumerableWithSyncSelector<TSource, TKey> : OrderedAsyncEnumerable<TSource, TKey>
    {
        private readonly Func<TSource, TKey> _keySelector;

        public OrderedAsyncEnumerableWithSyncSelector(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TSource> parent = null)
            : base(source, comparer, descending, parent)
        {
            _keySelector = keySelector;
        }

        protected internal override AsyncEnumerableSorter<TSource> GetEnumerableSorter(AsyncEnumerableSorter<TSource> next)
        {
            var sorter = AsyncEnumerableSorter.Create<TSource, TKey>(_keySelector, _comparer, _descending, next);

            if (_parent != null)
            {
                sorter = _parent.GetEnumerableSorter(sorter);
            }

            return sorter;
        }
    }

    internal sealed class OrderedAsyncEnumerableWithAsyncSelector<TSource, TKey> : OrderedAsyncEnumerable<TSource, TKey>
    {
        private readonly Func<TSource, ValueTask<TKey>> _keySelector;

        public OrderedAsyncEnumerableWithAsyncSelector(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TSource> parent = null)
            : base(source, comparer, descending, parent)
        {
            _keySelector = keySelector;
        }

        protected internal override AsyncEnumerableSorter<TSource> GetEnumerableSorter(AsyncEnumerableSorter<TSource> next)
        {
            var sorter = AsyncEnumerableSorter.Create<TSource, TKey>(_keySelector, _comparer, _descending, next);

            if (_parent != null)
            {
                sorter = _parent.GetEnumerableSorter(sorter);
            }

            return sorter;
        }
    }

    internal abstract class OrderedAsyncEnumerable<TSource, TKey> : OrderedAsyncEnumerable<TSource>
    {
        protected readonly IComparer<TKey> _comparer;
        protected readonly bool _descending;
        protected readonly OrderedAsyncEnumerable<TSource> _parent;

        protected OrderedAsyncEnumerable(IAsyncEnumerable<TSource> source, IComparer<TKey> comparer, bool descending, OrderedAsyncEnumerable<TSource> parent = null)
            : base(source)
        {
            _comparer = comparer ?? Comparer<TKey>.Default;
            _descending = descending;
            _parent = parent;
        }
    }

    internal abstract class OrderedAsyncEnumerable<TSource> : IOrderedAsyncEnumerable<TSource>
    {
        private readonly IAsyncEnumerable<TSource> _source;

        protected OrderedAsyncEnumerable(IAsyncEnumerable<TSource> source)
        {
            _source = source;
        }

        public IOrderedAsyncEnumerable<TSource> CreateOrderedAsyncEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending) => new OrderedAsyncEnumerableWithSyncSelector<TSource, TKey>(_source, keySelector, comparer, descending, this);

        public IOrderedAsyncEnumerable<TSource> CreateOrderedAsyncEnumerable<TKey>(Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending) => new OrderedAsyncEnumerableWithAsyncSelector<TSource, TKey>(_source, keySelector, comparer, descending, this);

        public async IAsyncEnumerator<TSource> GetAsyncEnumerator()
        {
            var buffer = await _source.ToArrayAsync().ConfigureAwait(false);

            if (buffer.Length > 0)
            {
                var sorter = GetEnumerableSorter(next: null);

                int[] map = await sorter.SortAsync(buffer).ConfigureAwait(false);

                sorter = null;

                for (int i = 0; i < buffer.Length; i++)
                {
                    yield return buffer[map[i]];
                }
            }
        }

        protected internal abstract AsyncEnumerableSorter<TSource> GetEnumerableSorter(AsyncEnumerableSorter<TSource> next);
    }

    internal static class AsyncEnumerableSorter
    {
        public static AsyncEnumerableSorter<TSource> Create<TSource, TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TSource> next) => new AsyncEnumerableSorterWithSyncSelector<TSource, TKey>(keySelector, comparer, descending, next);

        public static AsyncEnumerableSorter<TSource> Create<TSource, TKey>(Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TSource> next) => new AsyncEnumerableSorterWithAsyncSelector<TSource, TKey>(keySelector, comparer, descending, next);
    }

    internal abstract class AsyncEnumerableSorter<TSource>
    {
        internal abstract Task ComputeKeysAsync(TSource[] elements);

        internal abstract int CompareKeys(int index1, int index2);

        internal async Task<int[]> SortAsync(TSource[] elements)
        {
            await ComputeKeysAsync(elements).ConfigureAwait(false);

            int[] map = new int[elements.Length];

            for (int i = 0; i < elements.Length; i++)
            {
                map[i] = i;
            }

            QuickSort(map, 0, elements.Length - 1);

            return map;
        }

        private void QuickSort(int[] map, int left, int right)
        {
            do
            {
                int i = left;
                int j = right;
                int x = map[i + ((j - i) >> 1)];

                do
                {
                    while (i < map.Length && CompareKeys(x, map[i]) > 0) i++;
                    while (j >= 0 && CompareKeys(x, map[j]) < 0) j--;
                    if (i > j) break;
                    if (i < j)
                    {
                        int temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);

                if (j - left <= right - i)
                {
                    if (left < j) QuickSort(map, left, j);
                    left = i;
                }
                else
                {
                    if (i < right) QuickSort(map, i, right);
                    right = j;
                }
            } while (left < right);
        }
    }

    internal abstract class AsyncEnumerableSorter<TSource, TKey> : AsyncEnumerableSorter<TSource>
    {
        private readonly IComparer<TKey> _comparer;
        private readonly bool _descending;
        protected readonly AsyncEnumerableSorter<TSource> _next;
        protected TKey[] _keys;

        protected AsyncEnumerableSorter(IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TSource> next)
        {
            _comparer = comparer;
            _descending = descending;
            _next = next;
        }

        internal override int CompareKeys(int index1, int index2)
        {
            int c = _comparer.Compare(_keys[index1], _keys[index2]);

            if (c == 0)
            {
                if (_next == null)
                {
                    return index1 - index2;
                }

                return _next.CompareKeys(index1, index2);
            }

            return _descending ? -c : c;
        }
    }

    internal sealed class AsyncEnumerableSorterWithSyncSelector<TSource, TKey> : AsyncEnumerableSorter<TSource, TKey>
    {
        private readonly Func<TSource, TKey> _keySelector;

        public AsyncEnumerableSorterWithSyncSelector(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TSource> next)
            : base(comparer, descending, next)
        {
            _keySelector = keySelector;
        }

        internal override async Task ComputeKeysAsync(TSource[] elements)
        {
            _keys = new TKey[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                _keys[i] = _keySelector(elements[i]);
            }

            if (_next != null)
            {
                await _next.ComputeKeysAsync(elements).ConfigureAwait(false);
            }
        }
    }

    internal sealed class AsyncEnumerableSorterWithAsyncSelector<TSource, TKey> : AsyncEnumerableSorter<TSource, TKey>
    {
        private readonly Func<TSource, ValueTask<TKey>> _keySelector;

        public AsyncEnumerableSorterWithAsyncSelector(Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TSource> next)
            : base(comparer, descending, next)
        {
            _keySelector = keySelector;
        }

        internal override async Task ComputeKeysAsync(TSource[] elements)
        {
            _keys = new TKey[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                _keys[i] = await _keySelector(elements[i]).ConfigureAwait(false);
            }

            if (_next != null)
            {
                await _next.ComputeKeysAsync(elements).ConfigureAwait(false);
            }
        }
    }
}
