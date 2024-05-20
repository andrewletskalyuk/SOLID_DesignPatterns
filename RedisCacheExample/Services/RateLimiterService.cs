using StackExchange.Redis;

namespace RedisCacheExample.Services;

public class RateLimiterService
{
    private readonly IDatabase _redisDb;

    public RateLimiterService(IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
    }

    public async Task<bool> IsRequestAllowedAsync(string userId, int maxRequests, TimeSpan timeWindow)
    {
        string key = $"rate_limit:{userId}";

        // Increment the request count for the user
        var requestCount = await _redisDb.StringIncrementAsync(key);

        if (requestCount == 1)
        {
            // Set the expiration for the key
            await _redisDb.KeyExpireAsync(key, timeWindow);
        }

        // Check if the request count exceeds the limit
        return requestCount <= maxRequests; 
    }
}
