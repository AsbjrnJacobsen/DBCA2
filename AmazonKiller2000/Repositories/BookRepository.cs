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
        if (await dbContext.Books.AnyAsync(b => b.ISBN == idNr))
        {
            var res = await dbContext.Books.Include(b => b.Author).FirstAsync(b => b.ISBN == idNr);
            return res;
        }
        return null;
    }

    public async Task UpdateAsync(Book entity)
    {
        dbContext.Books.Update(entity);
        await SaveAsync();
    }

    public async Task DeleteAsync(int idNr)
    {
        var book = await ReadAsync(idNr);
        if (book is null)
            return;
        
        dbContext.Books.Remove(book);
        await SaveAsync();
    }
    
    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}