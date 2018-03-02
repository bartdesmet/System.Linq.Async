using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> SumAsync(this IAsyncEnumerable<int> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int> Core()
            {
                token.ThrowIfCancellationRequested();

                int sum = 0;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        sum += item;
                    }
                }

                return sum;
            }
        }

        public static Task<long> SumAsync(this IAsyncEnumerable<long> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        sum += item;
                    }
                }

                return sum;
            }
        }

        public static Task<float> SumAsync(this IAsyncEnumerable<float> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    sum += item;
                }

                return (float)sum;
            }
        }

        public static Task<double> SumAsync(this IAsyncEnumerable<double> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    sum += item;
                }

                return sum;
            }
        }

        public static Task<decimal> SumAsync(this IAsyncEnumerable<decimal> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal sum = 0.0m;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    sum += item;
                }

                return sum;
            }
        }

        public static Task<int?> SumAsync(this IAsyncEnumerable<int?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int?> Core()
            {
                token.ThrowIfCancellationRequested();

                int sum = 0;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (item.HasValue)
                        {
                            sum += item.GetValueOrDefault();
                        }
                    }
                }

                return sum;
            }
        }

        public static Task<long?> SumAsync(this IAsyncEnumerable<long?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long?> Core()
            {
                token.ThrowIfCancellationRequested();

                long sum = 0L;

                checked
                {
                    foreach await (var item in source.ConfigureAwait(false))
                    {
                        token.ThrowIfCancellationRequested();

                        if (item.HasValue)
                        {
                            sum += item.GetValueOrDefault();
                        }
                    }
                }

                return sum;
            }
        }

        public static Task<float?> SumAsync(this IAsyncEnumerable<float?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float?> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue)
                    {
                        sum += item.GetValueOrDefault();
                    }
                }

                return (float?)sum;
            }
        }

        public static Task<double?> SumAsync(this IAsyncEnumerable<double?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                double sum = 0.0;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue)
                    {
                        sum += item.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        public static Task<decimal?> SumAsync(this IAsyncEnumerable<decimal?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal?> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal sum = 0.0m;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue)
                    {
                        sum += item.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        public static Task<int> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<int> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<long> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<long> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<float> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<float> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<double> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<double> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<decimal> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<decimal> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<int?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<int?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<long?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<long?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<float?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<float?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<double?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<double?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<decimal?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

        public static Task<decimal?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken token = default) => source.Select(selector).SumAsync(token);

    }
}