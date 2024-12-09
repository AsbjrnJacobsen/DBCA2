using AmazonKiller2000.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKiller2000.Repositories;

public class BookRepository(PostgresDBContext dbContext) : IRepository<Book>
{
    public async Task CreateAsync(Book entity)
    {
        await dbContext.Books.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<Book?> ReadAsync(int idNr)
    {
        return await dbContext.Books.Include(b => b.Author).FirstAsync(b => b.ISBN == idNr);
    }

    public async Task UpdateAsync(Book entity)
    {
        dbContext.Books.Update(entity);
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