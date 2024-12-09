namespace AmazonKiller2000.Repositories;

public interface IRepository<T>
{
    Task CreateAsync(T entity);
    Task<T?> ReadAsync(int idNr);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int idNr);
    Task SaveAsync();
}