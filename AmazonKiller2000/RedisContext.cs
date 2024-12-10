using StackExchange.Redis;

namespace AmazonKiller2000;

public class RedisContext
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _password;

    private ConnectionMultiplexer? _redis;

    public RedisContext(string hostname, int port, string password)
    {
        _hostname = hostname;
        _port = port;
        _password = password;
        
        Connect();
    }

    private void Connect()
    {
        var connectionString = $"{_hostname}:{_port},password={_password}";
        _redis = ConnectionMultiplexer.Connect(connectionString);
    }

    public IDatabase GetDatabase()
    {
        if (_redis is null)
            Connect();
        return _redis is not null ? _redis.GetDatabase() : throw new NullReferenceException("Redis connection is not initialized.");
    }
}