using AmazonKiller2000.Models;

namespace AmazonKiller2000.Repositories;

public class BookRepository : IRepository<Book>
{
    
    public Task CreateAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task<Book> ReadAsync(int idNr)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Book entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int idNr)
    {
        throw new NotImplementedException();
    }
}