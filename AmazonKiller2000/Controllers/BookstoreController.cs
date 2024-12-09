using AmazonKiller2000.Models;
using AmazonKiller2000.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AmazonKiller2000.Controllers;

[ApiController]
public class BookstoreController(BookRepository booksRepo, AuthorRepository authorsRepo) : ControllerBase
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
        var book = await booksRepo.ReadAsync(bookId);        
        return book is null ? Ok(book) : NotFound();
    }
    
    [HttpPut("UpdateBook")]
    public async Task<ActionResult> UpdateBook([FromBody] Book book)
    {
        await booksRepo.UpdateAsync(book);        
        return Ok();
    }
    
    [HttpDelete("DeleteBook")]
    public async Task<ActionResult> DeleteBook([FromQuery] int bookId)
    {
        await booksRepo.DeleteAsync(bookId);        
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
        var book = await authorsRepo.ReadAsync(authorId);
        return book is null ? Ok(book) : NotFound();
    }
    
    [HttpPut("UpdateAuthor")]
    public async Task<ActionResult> UpdateAuthor([FromBody] Author author)
    {
        await authorsRepo.UpdateAsync(author);
        return Ok();
    }
    
    [HttpDelete("DeleteAuthor")]
    public async Task<ActionResult> DeleteAuthor([FromQuery] int authorId)
    {
        await authorsRepo.DeleteAsync(authorId);
        return Ok();
    }
}