using System.Text.Json;
using AmazonKiller2000.Models;

namespace AmazonKiller2000.Repositories.Redis;

public class BookCacheRepository(RedisContext redisContext) : StringCacheRepository(redisContext)
{
    public void StoreBook(Book book)
    {
        var jsonBook = JsonSerializer.Serialize(book);
        Store("book-" +  book.ISBN.ToString(), jsonBook);
    }

    public Book? GetBook(string isbn)
    {
        var jsonBook = Get("book-" + isbn);
        if (jsonBook is null)
            return null;

        jsonBook = jsonBook.Replace("book-", "");
        
        Book? book = JsonSerializer.Deserialize<Book>(jsonBook);
        return book;
    }

    public void RemoveBook(string isbn)
    {
        Remove("book-" + isbn);
    }
}