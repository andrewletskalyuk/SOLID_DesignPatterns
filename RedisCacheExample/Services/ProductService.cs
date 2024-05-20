using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisCacheExample.Services;

public class ProductService
{
    private readonly AppDbContext _dbContext;

    private readonly IDatabase _redisDb;

    public ProductService(AppDbContext dbContext, IConnectionMultiplexer redis)
    {
        _dbContext = dbContext;
        _redisDb = redis.GetDatabase();
    }

    public async Task<Entities.Product> GetProductByIdAsync(int id)
    {
        string cacheKey = $"product:{id}";

        // Try to get the product from Redis cache
        var cachedProduct = await _redisDb.StringGetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedProduct))
        {
            return JsonConvert.DeserializeObject<Entities.Product>(cachedProduct);
        }

        // If not found in cache, get it from the database
        var product = await _dbContext.Products.FindAsync(id);

        // Store the product in Redis cache for future requests
        if (product != null)
        {
            await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(product), TimeSpan.FromMinutes(5));
        }

        return product;
    }
}
