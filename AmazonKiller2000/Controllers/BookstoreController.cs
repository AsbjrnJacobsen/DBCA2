using AmazonKiller2000.Models;
using AmazonKiller2000.Repositories;
using AmazonKiller2000.Repositories.Redis;
using Microsoft.AspNetCore.Mvc;

namespace AmazonKiller2000.Controllers;

[ApiController]
[Route("[controller]")]
public class BookstoreController(
    BookRepository booksRepo, 
    BookCacheRepository booksCache, 
    AuthorRepository authorsRepo, 
    AuthorCacheRepository authorsCache) : ControllerBase
{
    [HttpPost("CreateBook")]
    public async Task<ActionResult> CreateBook([FromBody] Book book)
    {
        await booksRepo.CreateAsync(book);
        return Ok();
    }
    
    [HttpGet("GetBook")]
    public async Task<ActionResult<Book>> GetBook([FromQuery] int bookId)
    {
        var cache = booksCache.GetBook(bookId.ToString());
        if (cache is not null)
            return Ok(cache);
        
        var book = await booksRepo.ReadAsync(bookId);
        if (book is null) return NotFound();
        booksCache.StoreBook(book);
        return Ok(book);
    }
    
    [HttpPut("UpdateBook")]
    public async Task<ActionResult> UpdateBook([FromBody] Book book)
    {
        await booksRepo.UpdateAsync(book);
        booksCache.RemoveBook(book.ISBN.ToString());
        booksCache.StoreBook(book);
        return Ok();
    }
    
    [HttpDelete("DeleteBook")]
    public async Task<ActionResult> DeleteBook([FromQuery] int bookId)
    {
        await booksRepo.DeleteAsync(bookId);
        booksCache.RemoveBook(bookId.ToString());
        return Ok();
    }
    
    [HttpPost("CreateAuthor")]
    public async Task<ActionResult> CreateAuthor([FromBody] Author author)
    {
        await authorsRepo.CreateAsync(author);
        return Ok();
    }
    
    [HttpGet("GetAuthor")]
    public async Task<ActionResult<Author>> GetAuthor([FromQuery] int authorId)
    {
        var cache = authorsCache.GetAuthor(authorId);
        if (cache is not null)
            return Ok(cache);
        
        var author = await authorsRepo.ReadAsync(authorId);
        if (author is null) return NotFound();
        authorsCache.StoreAuthor(author);
        return Ok(author);
    }
    
    [HttpPut("UpdateAuthor")]
    public async Task<ActionResult> UpdateAuthor([FromBody] Author author)
    {
        await authorsRepo.UpdateAsync(author);
        authorsCache.RemoveAuthor(author.Id);
        authorsCache.StoreAuthor(author);
        return Ok();
    }
    
    [HttpDelete("DeleteAuthor")]
    public async Task<ActionResult> DeleteAuthor([FromQuery] int authorId)
    {
        await authorsRepo.DeleteAsync(authorId);
        authorsCache.RemoveAuthor(authorId);
        return Ok();
    }
}