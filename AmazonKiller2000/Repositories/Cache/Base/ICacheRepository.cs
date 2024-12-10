namespace AmazonKiller2000.Repositories.Redis;

public interface ICacheRepository<T>
{
    void Store(T key, T value);
    T? Get(T key);
    void Remove(T key);
}