using Microsoft.Extensions.Caching.Memory;
using TaskManagement.Application.Services.Contracts.Cache;

namespace TaskManagement.Application.Services.Implementations.Cache;

public class InMemmoryService(IMemoryCache cache) : ICacheService
{
    #region Get

    public Task<T?> GetAsync<T>(string key)
    {
        cache.TryGetValue(key, out T? value);

        return Task.FromResult(value);
    }

    #endregion Get

    #region Remove

    public Task RemoveAsync(string key)
    {
        cache.Remove(key);

        return Task.CompletedTask;
    }

    #endregion Remove

    #region Set 

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();

        if (expiration.HasValue)
            options.AbsoluteExpirationRelativeToNow = expiration;

        cache.Set(key, value, options);

        return Task.CompletedTask;
    }

    #endregion Set 
}
