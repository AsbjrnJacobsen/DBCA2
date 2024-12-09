using AmazonKiller2000.Models;
using MongoDB.Driver;

namespace AmazonKiller2000.Repositories;

public class OrderRepository(MongoDBContext dbContext) : IRepository<Order>
{
    public async Task CreateAsync(Order entity)
    {
        await dbContext.Collection<Order>().InsertOneAsync(entity);
    }

    public async Task<Order?> ReadAsync(int idNr)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.Id, idNr);
        return (await dbContext.Collection<Order>().FindAsync(filter)).First();
    }

    public async Task UpdateAsync(Order entity)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.Id, entity.Id);
        var updateDefinition = Builders<Order>.Update
            .Set(o => o.CustomerId, entity.CustomerId)
            .Set(o => o.IsPaid, entity.IsPaid)
            .Set(o => o.Items, entity.Items)
            .Set(o => o.TotalPrice, entity.TotalPrice);
        await dbContext.Collection<Order>().UpdateOneAsync(filter, updateDefinition);
    }

    public async Task DeleteAsync(int idNr)
    {
        await dbContext.Collection<Order>().DeleteOneAsync(x => x.Id == idNr);
    }

    public Task SaveAsync()
    {
        // MongoDB automatically saves changes. NOP
        return Task.CompletedTask;
    }
}