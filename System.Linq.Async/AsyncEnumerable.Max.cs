using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Async
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> MaxAsync(this IAsyncEnumerable<int> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int> Core()
            {
                token.ThrowIfCancellationRequested();

                int max = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item > max)
                        {
                            max = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        max = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return max;
            }
        }

        public static Task<long> MaxAsync(this IAsyncEnumerable<long> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long> Core()
            {
                token.ThrowIfCancellationRequested();

                long max = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item > max)
                        {
                            max = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        max = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return max;
            }
        }

        public static Task<float> MaxAsync(this IAsyncEnumerable<float> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float> Core()
            {
                token.ThrowIfCancellationRequested();

                float max = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item > max || float.IsNaN(item))
                        {
                            max = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        max = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return max;
            }
        }

        public static Task<double> MaxAsync(this IAsyncEnumerable<double> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                double max = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item > max || double.IsNaN(item))
                        {
                            max = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        max = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return max;
            }
        }

        public static Task<decimal> MaxAsync(this IAsyncEnumerable<decimal> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal max = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item > max)
                        {
                            max = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        max = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return max;
            }
        }

        public static Task<int?> MaxAsync(this IAsyncEnumerable<int?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int?> Core()
            {
                token.ThrowIfCancellationRequested();

                int? max = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!max.HasValue || item > max)
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        public static Task<long?> MaxAsync(this IAsyncEnumerable<long?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long?> Core()
            {
                token.ThrowIfCancellationRequested();

                long? max = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!max.HasValue || item > max)
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        public static Task<float?> MaxAsync(this IAsyncEnumerable<float?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float?> Core()
            {
                token.ThrowIfCancellationRequested();

                float? max = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue && (!max.HasValue || item > max || float.IsNaN(item.Value)))
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        public static Task<double?> MaxAsync(this IAsyncEnumerable<double?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                double? max = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue && (!max.HasValue || item > max || double.IsNaN(item.Value)))
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        public static Task<decimal?> MaxAsync(this IAsyncEnumerable<decimal?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal?> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal? max = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!max.HasValue || item > max)
                    {
                        max = item;
                    }
                }

                return max;
            }
        }

        public static Task<int> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<int> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<int?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<int?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<long> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<long> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<long?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<long?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<float> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<float> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<float?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<float?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<double> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<double> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<double?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<double?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<decimal> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<decimal> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<decimal?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

        public static Task<decimal?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken token = default) => source.Select(selector).MaxAsync(token);

    }
}
