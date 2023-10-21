using Microsoft.Extensions.Caching.Distributed;

namespace SuperPlay.Abstractions.Extensions;

public static class DistributedCacheExtensions
{
    public static async ValueTask SetEntityAsync(
        this IDistributedCache context,
        string key,
        object value,
        CancellationToken token = default)
    {
        var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value);
        await  context.SetAsync(key, bytes, token);
    }
    
    public static async ValueTask<T?> GetEntityAsync<T>(
        this IDistributedCache context,
        string key,
        CancellationToken token = default)
    {
        var bytes = await context.GetAsync(key, token);;

        return bytes is not null ? System.Text.Json.JsonSerializer.Deserialize<T>(bytes) : default;
    }
}