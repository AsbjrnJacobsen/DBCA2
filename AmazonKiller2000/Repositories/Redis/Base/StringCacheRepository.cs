using StackExchange.Redis;

namespace AmazonKiller2000.Repositories.Redis;

public class StringCacheRepository(RedisContext redisContext) : ICacheRepository<string>
{
    public void Store(string key, string value)
    {
        var db = redisContext.GetDatabase();
        db.StringSet(key, value);
    }

    public string? Get(string key)
    {
        var db = redisContext.GetDatabase();
        return db.StringGet(new RedisKey(key));
    }

    public void Remove(string key)
    {
        var db = redisContext.GetDatabase();
        db.KeyDelete(key);
    }
}