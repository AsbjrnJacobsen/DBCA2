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
        return await dbContext.Authors.FindAsync(idNr);
    }

    public async Task UpdateAsync(Author entity)
    {
        dbContext.Authors.Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(int idNr)
    {
        await dbContext.Books.Select(b => b.ISBN == idNr).ExecuteDeleteAsync();
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}