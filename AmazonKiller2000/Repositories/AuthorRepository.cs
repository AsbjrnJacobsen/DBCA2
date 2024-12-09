using AmazonKiller2000.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKiller2000.Repositories;

public class AuthorRepository(PostgresDBContext dbContext) : IRepository<Author>
{
    public async Task CreateAsync(Author entity)
    {
        await dbContext.Authors.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<Author?> ReadAsync(int idNr)
    {
        if (await dbContext.Authors.AnyAsync(b => b.Id == idNr))
        {
            var res = await dbContext.Authors.FirstAsync(b => b.Id == idNr);
            return res;
        }
        return null;
    }

    public async Task UpdateAsync(Author entity)
    {
        dbContext.Authors.Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(int idNr)
    {
        var author = await ReadAsync(idNr);
        
        if (author is null)
            return;
        
        dbContext.Authors.Remove(author);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}