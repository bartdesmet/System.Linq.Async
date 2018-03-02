using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static Task<double> AverageAsync(this IAsyncEnumerable<int> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        sum += item;
                        count++;
                    }
                }

                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double> AverageAsync(this IAsyncEnumerable<long> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        sum += item;
                        count++;
                    }
                }

                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<float> AverageAsync(this IAsyncEnumerable<float> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;
                long count = 0L;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    sum += item;
                    checked { count++; }
                }

                if (count > 0L)
                {
                    return (float)((double)sum / (double)count);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double> AverageAsync(this IAsyncEnumerable<double> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;
                long count = 0L;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    sum += item;
                    checked { count++; }
                }

                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<decimal> AverageAsync(this IAsyncEnumerable<decimal> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal sum = 0.0m;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        sum += item;
                        count++;
                    }
                }

                if (count > 0L)
                {
                    return sum / count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double?> AverageAsync(this IAsyncEnumerable<int?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (item.HasValue)
                        {
                            sum += item.GetValueOrDefault();
                            count++;
                        }
                    }
                }
                
                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double?> AverageAsync(this IAsyncEnumerable<long?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (item.HasValue)
                        {
                            sum += item.GetValueOrDefault();
                            count++;
                        }
                    }
                }
                
                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<float?> AverageAsync(this IAsyncEnumerable<float?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float?> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;
                long count = 0L;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue)
                    {
                        sum += item.GetValueOrDefault();
                        checked { count++; }
                    }
                }
                
                if (count > 0L)
                {
                    return (float)((double)sum / (double)count);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double?> AverageAsync(this IAsyncEnumerable<double?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;
                long count = 0L;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue)
                    {
                        sum += item.GetValueOrDefault();
                        checked { count++; }
                    }
                }
                
                if (count > 0L)
                {
                    return (double)sum / (double)count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<decimal?> AverageAsync(this IAsyncEnumerable<decimal?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal?> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal sum = 0.0m;
                long count = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (item.HasValue)
                        {
                            sum += item.GetValueOrDefault();
                            count++;
                        }
                    }
                }
                
                if (count > 0L)
                {
                    return sum / count;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

        public static Task<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken token = default) => source.Select(selector).AverageAsync(token);

    }
}
