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

    public async void ApplyDataSeed()
    {
        // Drop database seed in case it has already been applied
        await _client.DropDatabaseAsync(_databaseName);
        
        // Customers
        var customerCollection = _client.GetDatabase(_databaseName).GetCollection<Customer>(nameof(Customer));
        
        Customer c1 = new Customer{ Id = 1, Name = "John Johnson", Email = "john.johnson@gmail.com", Telephone = 12345678, Address = "Johnsonstreet 42" };
        Customer c2 = new Customer{ Id = 2, Name = "Jane Doe", Email = "janedoe@mail.com", Telephone = 12345678, Address = "Green Aveneue 14" };
        
        await customerCollection.InsertOneAsync(c1);
        await customerCollection.InsertOneAsync(c2);
        
        // Orders
        var orderCollection = _client.GetDatabase(_databaseName).GetCollection<Order>(nameof(Order));
        
        Order o1 = new Order() { Id = 1, CustomerId = c1.Id, IsPaid = true, Items = [1, 2, 3, 4], TotalPrice = 400 };
        Order o2 = new Order() { Id = 2, CustomerId = c2.Id, IsPaid = false, Items = [5, 6], TotalPrice = 250 };
        
        await orderCollection.InsertOneAsync(o1);
        await orderCollection.InsertOneAsync(o2);
    }
}