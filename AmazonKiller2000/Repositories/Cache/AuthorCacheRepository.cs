using System.Text.Json;
using AmazonKiller2000.Models;

namespace AmazonKiller2000.Repositories.Redis;

public class AuthorCacheRepository(RedisContext redisContext) : StringCacheRepository(redisContext)
{
    public void StoreAuthor(Author author)
    {
        var jsonAuthor = JsonSerializer.Serialize(author);
        Store("author-" + author.Id.ToString(), jsonAuthor);
    }

    public Author? GetAuthor(int id)
    {
        var jsonAuthor = Get("author-" + id);
        if (jsonAuthor is null)
            return null;

        jsonAuthor = jsonAuthor.Replace("author-", "");
        
        Author? author = JsonSerializer.Deserialize<Author>(jsonAuthor);
        return author;
    }

    public void RemoveAuthor(int id)
    {
        Remove("author-" + id.ToString());
    }
}