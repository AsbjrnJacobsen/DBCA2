using AmazonKiller2000.Models;
using MongoDB.Driver;

namespace AmazonKiller2000.Repositories;

public class CustomerRepository(MongoDBContext dbContext) : IRepository<Customer>
{
    public async Task CreateAsync(Customer entity)
    {
        await dbContext.Collection<Customer>().InsertOneAsync(entity);
    }

    public async Task<Customer?> ReadAsync(int idNr)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Id, idNr);
        return (await dbContext.Collection<Customer>().FindAsync(filter)).First();
    }

    public async Task UpdateAsync(Customer entity)
    {
        var filter = Builders<Customer>.Filter.Eq(x => x.Id, entity.Id);
        var updateDefinition = Builders<Customer>.Update
            .Set(c => c.Email, entity.Email)
            .Set(c => c.Address, entity.Address)
            .Set(c => c.Name, entity.Name)
            .Set(c => c.Telephone, entity.Telephone);
        await dbContext.Collection<Customer>().UpdateOneAsync(filter, updateDefinition);
    }

    public async Task DeleteAsync(int idNr)
    {
        await dbContext.Collection<Customer>().DeleteOneAsync(x => x.Id == idNr);
    }

    public Task SaveAsync()
    {
        // MongoDB automatically saves changes. NOP
        return Task.CompletedTask;
    }
}