using System.Diagnostics;
using System.Linq.Expressions;
using AmazonKiller2000.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AmazonKiller2000;

public class MongoDBContext
{
    private readonly string _connectionString;
    private readonly IMongoClient _client;

    private readonly string _databaseName;


    public MongoDBContext(string connectionString, string databaseName)
    {
        _databaseName = databaseName;
        _connectionString = connectionString;
        _client = new MongoClient(_connectionString);
    }
    public IMongoCollection<T> Collection<T>()
    {
        return _client.GetDatabase(_databaseName).GetCollection<T>((typeof(T).Name));
    }

    public void ApplyDataSeed()
    {
        var customerCollection = _client.GetDatabase(_databaseName).GetCollection<Customer>(nameof(Customer));
        Customer c1 = new Customer{Name = "John Johnson", Email = "john.johnson@gmail.com", Telephone = 12345678, Address = "Johnsonstreet 42",};
        
        var orderCollection = _client.GetDatabase(_databaseName).GetCollection<Order>(nameof(Order));
        
    }
}