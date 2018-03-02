using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static Task<TSource> AggregateAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, TSource> func, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (!await enumerator.MoveNextAsync())
                    {
                        throw new InvalidOperationException();
                    }

                    token.ThrowIfCancellationRequested();

                    var result = enumerator.Current;

                    while (await enumerator.MoveNextAsync())
                    {
                        token.ThrowIfCancellationRequested();

                        result = func(result, enumerator.Current);
                    }

                    return result;
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }
            }
        }

        public static Task<TSource> AggregateAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, ValueTask<TSource>> func, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (!await enumerator.MoveNextAsync())
                    {
                        throw new InvalidOperationException();
                    }

                    token.ThrowIfCancellationRequested();

                    var result = enumerator.Current;

                    while (await enumerator.MoveNextAsync())
                    {
                        token.ThrowIfCancellationRequested();

                        result = await func(result, enumerator.Current).ConfigureAwait(false);

                        token.ThrowIfCancellationRequested();
                    }

                    return result;
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }
            }
        }

        public static Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return Core();

            async Task<TAccumulate> Core()
            {
                token.ThrowIfCancellationRequested();

                var result = seed;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    result = func(result, item);
                }

                return result;
            }
        }

        public static Task<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> func, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return Core();

            async Task<TAccumulate> Core()
            {
                token.ThrowIfCancellationRequested();

                var result = seed;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    result = await func(result, item).ConfigureAwait(false);

                    token.ThrowIfCancellationRequested();
                }

                return result;
            }
        }

        public static Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Core();

            async Task<TResult> Core()
            {
                token.ThrowIfCancellationRequested();

                var result = seed;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    result = func(result, item);
                }

                return resultSelector(result);
            }
        }

        public static Task<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> func, Func<TAccumulate, ValueTask<TResult>> resultSelector, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Core();

            async Task<TResult> Core()
            {
                token.ThrowIfCancellationRequested();

                var result = seed;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    result = await func(result, item).ConfigureAwait(false);

                    token.ThrowIfCancellationRequested();
                }

                return await resultSelector(result).ConfigureAwait(false);
            }
        }

        public static Task<bool> AllAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!predicate(item))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static Task<bool> AllAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!await predicate(item).ConfigureAwait(false))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static Task<bool> AnyAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    return await enumerator.MoveNextAsync();
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }
            }
        }

        public static Task<bool> AnyAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static Task<bool> AnyAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!await predicate(item).ConfigureAwait(false))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static IAsyncEnumerable<TSource> Append<TSource>(this IAsyncEnumerable<TSource> source, TSource element)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return item;
                }

                yield return element;
            }
        }

        public static IAsyncEnumerable<TSource> AsyncAsyncEnumerable<TSource>(this IAsyncEnumerable<TSource> source) => source;

        public static IAsyncEnumerable<TSource> Concat<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in first.ConfigureAwait(false))
                {
                    yield return item;
                }

                foreach await (var item in second.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        public static Task<bool> ContainsAsync<TSource>(this IAsyncEnumerable<TSource> source, TSource value, CancellationToken token = default)
        {
            return ContainsAsync(source, value, null, token);
        }

        public static Task<bool> ContainsAsync<TSource>(this IAsyncEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (comparer.Equals(item, value))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static Task<int> CountAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int> Core()
            {
                var count = 0;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        count++;
                    }
                }

                return count;
            }
        }

        public static Task<int> CountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<int> Core()
            {
                var count = 0;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (predicate(item))
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public static Task<int> CountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<int> Core()
            {
                var count = 0;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (await predicate(item).ConfigureAwait(false))
                        {
                            token.ThrowIfCancellationRequested();

                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public static IAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IAsyncEnumerable<TSource> source)
        {
            return DefaultIfEmpty(source, default);
        }

        public static IAsyncEnumerable<TSource> DefaultIfEmpty<TSource>(this IAsyncEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();

                try
                {
                    if (await enumerator.MoveNextAsync())
                    {
                        do
                        {
                            yield return enumerator.Current;
                        }
                        while (await enumerator.MoveNextAsync());
                    }
                    else
                    {
                        yield return defaultValue;
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }
            }
        }

        public static IAsyncEnumerable<TSource> Distinct<TSource>(this IAsyncEnumerable<TSource> source)
        {
            return Distinct(source, null);
        }

        public static IAsyncEnumerable<TSource> Distinct<TSource>(this IAsyncEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var set = new HashSet<TSource>(comparer);

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (set.Add(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static Task<TSource> ElementAtAsync<TSource>(this IAsyncEnumerable<TSource> source, int index, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (index-- == 0)
                    {
                        return item;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static Task<TSource> ElementAtOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, int index, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                if (index >= 0)
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (index-- == 0)
                        {
                            return item;
                        }
                    }
                }

                return default;
            }
        }

        public static IAsyncEnumerable<TResult> Empty<TResult>()
        {
            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                yield break;
            }
        }

        public static IAsyncEnumerable<TSource> Except<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second)
        {
            return Except(first, second, null);
        }

        public static IAsyncEnumerable<TSource> Except<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var set = new HashSet<TSource>(comparer);

                foreach await (var item in second.ConfigureAwait(false))
                {
                    set.Add(item);
                }

                foreach await (var item in first.ConfigureAwait(false))
                {
                    if (set.Add(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static Task<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (await enumerator.MoveNextAsync())
                    {
                        token.ThrowIfCancellationRequested();

                        return enumerator.Current;
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        return item;
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        return item;
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (await enumerator.MoveNextAsync())
                    {
                        return enumerator.Current;
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                return default;
            }
        }

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        return item;
                    }
                }

                return default;
            }
        }

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        return item;
                    }
                }

                return default;
            }
        }

        // GroupBy
        // GroupJoin

        public static IAsyncEnumerable<TSource> Intersect<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second)
        {
            return Intersect(first, second, null);
        }

        public static IAsyncEnumerable<TSource> Intersect<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var set = new HashSet<TSource>(comparer);

                foreach await (var item in first.ConfigureAwait(false))
                {
                    set.Add(item);
                }

                foreach await (var item in second.ConfigureAwait(false))
                {
                    if (set.Remove(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        // Join

        public static Task<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (await enumerator.MoveNextAsync())
                    {
                        TSource current;

                        do
                        {
                            token.ThrowIfCancellationRequested();

                            current = enumerator.Current;
                        } while (await enumerator.MoveNextAsync());
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var flag = false;
                TSource last = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        flag = true;
                        last = item;
                    }
                }

                if (!flag)
                {
                    throw new InvalidOperationException();
                }

                return last;
            }
        }

        public static Task<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var flag = false;
                TSource last = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        flag = true;
                        last = item;
                    }
                }

                if (!flag)
                {
                    throw new InvalidOperationException();
                }

                return last;
            }
        }

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (await enumerator.MoveNextAsync())
                    {
                        TSource current;

                        do
                        {
                            token.ThrowIfCancellationRequested();

                            current = enumerator.Current;
                        } while (await enumerator.MoveNextAsync());
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                return default;
            }
        }

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                TSource last = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        last = item;
                    }
                }

                return last;
            }
        }

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                TSource last = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        last = item;
                    }
                }

                return last;
            }
        }

        public static Task<long> LongCountAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long> Core()
            {
                var count = 0L;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        count++;
                    }
                }

                return count;
            }
        }

        public static Task<long> LongCountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<long> Core()
            {
                var count = 0L;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (predicate(item))
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public static Task<long> LongCountAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<long> Core()
            {
                var count = 0L;

                checked
                {
                    token.ThrowIfCancellationRequested();

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (await predicate(item).ConfigureAwait(false))
                        {
                            token.ThrowIfCancellationRequested();

                            count++;
                        }
                    }
                }

                return count;
            }
        }

        public static Task<TSource> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                var comparer = Comparer<TSource>.Default;

                var max = default(TSource);

                if (max == null)
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        if (item != null && (max == null || comparer.Compare(item, max) > 0))
                        {
                            max = item;
                        }
                    }

                    return max;
                }
                else
                {
                    var hasValue = false;

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        if (hasValue)
                        {
                            if (comparer.Compare(item, max) > 0)
                            {
                                max = item;
                            }
                        }
                        else
                        {
                            max = item;
                            hasValue = true;
                        }
                    }

                    if (hasValue)
                    {
                        return max;
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TResult> MaxAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<TResult> MaxAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<TSource> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                var comparer = Comparer<TSource>.Default;

                var min = default(TSource);

                if (min == null)
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        if (item != null && (min == null || comparer.Compare(item, min) < 0))
                        {
                            min = item;
                        }
                    }

                    return min;
                }
                else
                {
                    var hasValue = false;

                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        if (hasValue)
                        {
                            if (comparer.Compare(item, min) < 0)
                            {
                                min = item;
                            }
                        }
                        else
                        {
                            min = item;
                            hasValue = true;
                        }
                    }

                    if (hasValue)
                    {
                        return min;
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer: null, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer: null, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer: null, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer: null, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return OrderedAsyncEnumerable.Create<TSource, TKey>(source, keySelector, comparer, descending: true);
        }

        public static IAsyncEnumerable<TSource> Prepend<TSource>(this IAsyncEnumerable<TSource> source, TSource element)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                yield return element;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }

        public static IAsyncEnumerable<int> Range(int start, int count) => Enumerable.Range(start, count).ToAsyncEnumerable();

        public static IAsyncEnumerable<TResult> Repeat<TResult>(TResult element, int count) => Enumerable.Repeat(element, count).ToAsyncEnumerable();

        public static IAsyncEnumerable<TSource> Reverse<TSource>(this IAsyncEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var list = new List<TSource>();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    list.Add(item);
                }

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    yield return list[i];
                }
            }
        }

        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return selector(item);
                }
            }
        }

        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return selector(item).ConfigureAwait(false);
                }
            }
        }

        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return selector(item, checked(index++));
                }
            }
        }

        public static IAsyncEnumerable<TResult> Select<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    yield return await selector(item, checked(index++)).ConfigureAwait(false);
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in selector(outer).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in selector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in collectionSelector(outer).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in collectionSelector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in selector(outer))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in selector(outer, checked(index++)))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in collectionSelector(outer))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in collectionSelector(outer, checked(index++)))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await selector(outer).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await selector(outer, checked(index++)).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await collectionSelector(outer).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await collectionSelector(outer, checked(index++)).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await selector(outer).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await selector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return inner;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await collectionSelector(outer).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await collectionSelector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in collectionSelector(outer).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in collectionSelector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in collectionSelector(outer))
                    {
                        yield return resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in collectionSelector(outer, checked(index++)))
                    {
                        yield return resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await collectionSelector(outer).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return await resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach await (var inner in (await collectionSelector(outer, checked(index++)).ConfigureAwait(false)).ConfigureAwait(false))
                    {
                        yield return await resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, Task<IEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await collectionSelector(outer).ConfigureAwait(false))
                    {
                        yield return await resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task<IEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (collectionSelector == null)
                throw new ArgumentNullException(nameof(collectionSelector));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var index = 0;

                foreach await (var outer in source.ConfigureAwait(false))
                {
                    foreach (var inner in await collectionSelector(outer, checked(index++)).ConfigureAwait(false))
                    {
                        yield return await resultSelector(outer, inner).ConfigureAwait(false);
                    }
                }
            }
        }

        public static Task<bool> SequenceEqualAsync<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second, CancellationToken token = default)
        {
            return SequenceEqualAsync(first, second, null, token);
        }

        public static Task<bool> SequenceEqualAsync<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer, CancellationToken token = default)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            return Core();

            async Task<bool> Core()
            {
                token.ThrowIfCancellationRequested();

                var firstEnumerator = first.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    var secondEnumerator = second.ConfigureAwait(false).GetAsyncEnumerator();
                    try
                    {
                        while (await firstEnumerator.MoveNextAsync())
                        {
                            token.ThrowIfCancellationRequested();

                            if (!await secondEnumerator.MoveNextAsync() || !comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                            {
                                return false;
                            }

                            token.ThrowIfCancellationRequested();
                        }

                        if (await secondEnumerator.MoveNextAsync())
                        {
                            return false;
                        }
                    }
                    finally
                    {
                        await secondEnumerator.DisposeAsync();
                    }
                }
                finally
                {
                    await firstEnumerator.DisposeAsync();
                }

                return true;
            }
        }

        public static Task<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (!await enumerator.MoveNextAsync())
                    {
                        throw new InvalidOperationException();
                    }

                    var current = enumerator.Current;

                    if (!await enumerator.MoveNextAsync())
                    {
                        return current;
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                long count = 0;
                TSource result = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        result = item;
                        checked { count++; }
                    }
                }

                if (count == 1)
                {
                    return result;
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                long count = 0;
                TSource result = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        result = item;
                        checked { count++; }
                    }
                }

                if (count == 1)
                {
                    return result;
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    if (!await enumerator.MoveNextAsync())
                    {
                        return default;
                    }

                    var current = enumerator.Current;

                    if (!await enumerator.MoveNextAsync())
                    {
                        return current;
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }

                throw new InvalidOperationException();
            }
        }

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                long count = 0;
                TSource result = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (predicate(item))
                    {
                        result = item;
                        checked { count++; }
                    }
                }

                if (count > 1)
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
        }

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Core();

            async Task<TSource> Core()
            {
                token.ThrowIfCancellationRequested();

                long count = 0;
                TSource result = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (await predicate(item).ConfigureAwait(false))
                    {
                        result = item;
                        checked { count++; }
                    }
                }

                if (count > 1)
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
        }

        public static IAsyncEnumerable<TSource> Skip<TSource>(this IAsyncEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var enumerator = source.ConfigureAwait(false).GetAsyncEnumerator();

                try
                {
                    while (count > 0 && await enumerator.MoveNextAsync())
                    {
                        count--;
                    }

                    if (count <= 0)
                    {
                        while (await enumerator.MoveNextAsync())
                        {
                            yield return enumerator.Current;
                        }
                    }
                }
                finally
                {
                    await enumerator.DisposeAsync();
                }
            }
        }

        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var flag = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!flag && !predicate(item))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var flag = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!flag && !await predicate(item).ConfigureAwait(false))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var flag = false;
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!flag && !predicate(item, checked(index++)))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var flag = false;
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!flag && !await predicate(item, checked(index++)).ConfigureAwait(false))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> Take<TSource>(this IAsyncEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                if (count > 0)
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        yield return item;

                        if (--count == 0)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!predicate(item))
                    {
                        break;
                    }

                    yield return item;
                }
            }
        }

        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!await predicate(item).ConfigureAwait(false))
                    {
                        break;
                    }

                    yield return item;
                }
            }
        }

        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!predicate(item, checked(index++)))
                    {
                        break;
                    }

                    yield return item;
                }
            }
        }

        public static IAsyncEnumerable<TSource> TakeWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (!await predicate(item, checked(index++)).ConfigureAwait(false))
                    {
                        break;
                    }

                    yield return item;
                }
            }
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer: null, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable<TKey>(keySelector, comparer: null, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer, descending: false);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer: null, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable<TKey>(keySelector, comparer: null, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer, descending: true);
        }

        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.CreateOrderedAsyncEnumerable(keySelector, comparer, descending: true);
        }

        public static Task<TSource[]> ToArrayAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<TSource[]> Core()
            {
                token.ThrowIfCancellationRequested();

                var list = new List<TSource>();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    list.Add(item);
                }

                return list.ToArray();
            }
        }

        public static IAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        // ToDictionary

        public static Task<IList<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<IList<TSource>> Core()
            {
                token.ThrowIfCancellationRequested();

                var list = new List<TSource>();

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    list.Add(item);
                }

                return list;
            }
        }

        // ToLookup

        public static IAsyncEnumerable<TSource> Union<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second)
        {
            return Union(first, second, null);
        }

        public static IAsyncEnumerable<TSource> Union<TSource>(this IAsyncEnumerable<TSource> first, IAsyncEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var set = new HashSet<TSource>(comparer);

                foreach await (var item in first.ConfigureAwait(false))
                {
                    if (set.Add(item))
                    {
                        set.Add(item);
                    }
                }

                foreach await (var item in second.ConfigureAwait(false))
                {
                    if (set.Add(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (await predicate(item).ConfigureAwait(false))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (predicate(item, checked(index++)))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TSource> Where<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            async IAsyncEnumerable<TSource> Iterator()
            {
                var index = 0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    if (await predicate(item, checked(index++)).ConfigureAwait(false))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var firstEnumerator = first.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    var secondEnumerator = second.ConfigureAwait(false).GetAsyncEnumerator();
                    try
                    {
                        while (await firstEnumerator.MoveNextAsync() && await secondEnumerator.MoveNextAsync())
                        {
                            yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
                        }
                    }
                    finally
                    {
                        await secondEnumerator.DisposeAsync();
                    }
                }
                finally
                {
                    await firstEnumerator.DisposeAsync();
                }
            }
        }

        public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, ValueTask<TResult>> resultSelector)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (resultSelector == null)
                throw new ArgumentNullException(nameof(resultSelector));

            return Iterator();

            async IAsyncEnumerable<TResult> Iterator()
            {
                var firstEnumerator = first.ConfigureAwait(false).GetAsyncEnumerator();
                try
                {
                    var secondEnumerator = second.ConfigureAwait(false).GetAsyncEnumerator();
                    try
                    {
                        while (await firstEnumerator.MoveNextAsync() && await secondEnumerator.MoveNextAsync())
                        {
                            yield return await resultSelector(firstEnumerator.Current, secondEnumerator.Current).ConfigureAwait(false);
                        }
                    }
                    finally
                    {
                        await secondEnumerator.DisposeAsync();
                    }
                }
                finally
                {
                    await firstEnumerator.DisposeAsync();
                }
            }
        }
    }
}
