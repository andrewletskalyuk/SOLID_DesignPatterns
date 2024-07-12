using StackExchange.Redis;

namespace RedisCacheExample.Services;

public class AnalyticsService
{
    private readonly IDatabase _redisDb;

    public AnalyticsService(IConnectionMultiplexer redis)
    {
        _redisDb = redis.GetDatabase();
    }

    public async Task RecordPageViewAsync(string page)
    {
        string key = $"page_views:{page}";
        await _redisDb.StringIncrementAsync(key);
    }

    public async Task<long> GetPageViewsAsync(string page)
    {
        string key = $"page_views:{page}";
        return (long)await _redisDb.StringGetAsync(key);
    }
}
