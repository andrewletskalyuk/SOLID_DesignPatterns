using StackExchange.Redis;

namespace RedisCacheExample.Services;

public class DistributedLockService
{
    private readonly IDatabase _redisDb;
    private readonly TimeSpan _lockTimeout;

    public DistributedLockService(IConnectionMultiplexer redis, TimeSpan lockTimeout)
    {
        _redisDb = redis.GetDatabase();
        _lockTimeout = lockTimeout;
    }

    public async Task<bool> AcquireLockAsync(string resource, string lockId)
    {
        return await _redisDb.StringSetAsync(resource, lockId, _lockTimeout, When.NotExists);
    }

    public async Task<bool> AcquireLockAsync(string lockKey, string lockValue, TimeSpan expiry)
    {
        return await _redisDb.StringSetAsync(lockKey, lockValue, expiry, When.NotExists);
    }

    public async Task<bool> ReleaseLockAsync(string resource, string lockId)
    {
        var tran = _redisDb.CreateTransaction();
        tran.AddCondition(Condition.StringEqual(resource, lockId));
        tran?.KeyDeleteAsync(resource);
        return await tran.ExecuteAsync();
    }
}
