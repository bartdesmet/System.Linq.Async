using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static Task<int> MinAsync(this IAsyncEnumerable<int> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int> Core()
            {
                token.ThrowIfCancellationRequested();

                int min = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item < min)
                        {
                            min = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        min = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return min;
            }
        }

        public static Task<long> MinAsync(this IAsyncEnumerable<long> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long> Core()
            {
                token.ThrowIfCancellationRequested();

                long min = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item < min)
                        {
                            min = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        min = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return min;
            }
        }

        public static Task<float> MinAsync(this IAsyncEnumerable<float> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float> Core()
            {
                token.ThrowIfCancellationRequested();

                float min = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item < min || float.IsNaN(item))
                        {
                            min = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        min = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return min;
            }
        }

        public static Task<double> MinAsync(this IAsyncEnumerable<double> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double> Core()
            {
                token.ThrowIfCancellationRequested();

                double min = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item < min || double.IsNaN(item))
                        {
                            min = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        min = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return min;
            }
        }

        public static Task<decimal> MinAsync(this IAsyncEnumerable<decimal> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal min = default;
                bool hasValue = false;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (hasValue)
                    {
                        if (item < min)
                        {
                            min = item;
                        }
                    }
                    else
                    {
                        hasValue = true;
                        min = item;
                    }
                }

                if (!hasValue)
                {
                    throw new InvalidOperationException();
                }

                return min;
            }
        }

        public static Task<int?> MinAsync(this IAsyncEnumerable<int?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<int?> Core()
            {
                token.ThrowIfCancellationRequested();

                int? min = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!min.HasValue || item < min)
                    {
                        min = item;
                    }
                }

                return min;
            }
        }

        public static Task<long?> MinAsync(this IAsyncEnumerable<long?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<long?> Core()
            {
                token.ThrowIfCancellationRequested();

                long? min = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!min.HasValue || item < min)
                    {
                        min = item;
                    }
                }

                return min;
            }
        }

        public static Task<float?> MinAsync(this IAsyncEnumerable<float?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<float?> Core()
            {
                token.ThrowIfCancellationRequested();

                float? min = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue && (!min.HasValue || item < min || float.IsNaN(item.Value)))
                    {
                        min = item;
                    }
                }

                return min;
            }
        }

        public static Task<double?> MinAsync(this IAsyncEnumerable<double?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<double?> Core()
            {
                token.ThrowIfCancellationRequested();

                double? min = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (item.HasValue && (!min.HasValue || item < min || double.IsNaN(item.Value)))
                    {
                        min = item;
                    }
                }

                return min;
            }
        }

        public static Task<decimal?> MinAsync(this IAsyncEnumerable<decimal?> source, CancellationToken token = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Core();

            async Task<decimal?> Core()
            {
                token.ThrowIfCancellationRequested();

                decimal? min = default;

                foreach await (var item in source.ConfigureAwait(false))
                {
                    token.ThrowIfCancellationRequested();

                    if (!min.HasValue || item < min)
                    {
                        min = item;
                    }
                }

                return min;
            }
        }

        public static Task<int> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<int> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<int?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<int?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<long> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<long> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<long?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<long?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<float> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<float> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<float?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<float?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<double> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<double> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<double?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<double?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<decimal> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<decimal> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<decimal?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

        public static Task<decimal?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken token = default) => source.Select(selector).MinAsync(token);

    }
}
