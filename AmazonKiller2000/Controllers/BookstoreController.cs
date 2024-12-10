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
    AuthorCacheRepository authorsCache,
    CustomerRepository customersRepo,
    OrderRepository ordersRepo) : ControllerBase
{
    // Books
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
        return Ok();
    }
    
    [HttpDelete("DeleteBook")]
    public async Task<ActionResult> DeleteBook([FromQuery] int bookId)
    {
        await booksRepo.DeleteAsync(bookId);
        booksCache.RemoveBook(bookId.ToString());
        return Ok();
    }
    
    // Authors
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
        return Ok();
    }
    
    [HttpDelete("DeleteAuthor")]
    public async Task<ActionResult> DeleteAuthor([FromQuery] int authorId)
    {
        await authorsRepo.DeleteAsync(authorId);
        authorsCache.RemoveAuthor(authorId);
        return Ok();
    }
    
    // Customers
    [HttpPost("CreateCustomer")]
    public async Task<ActionResult> CreateCustomer([FromBody] Customer customer)
    {
        await customersRepo.CreateAsync(customer);
        return Ok();
    }
    
    [HttpGet("GetCustomer")]
    public async Task<ActionResult<Customer>> GetCustomer([FromQuery] int customerId)
    {
        var customer = await customersRepo.ReadAsync(customerId);
        if (customer is null) return NotFound();
        return Ok(customer);
    }
    
    [HttpPut("UpdateCustomer")]
    public async Task<ActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        await customersRepo.UpdateAsync(customer);
        return Ok();
    }
    
    [HttpDelete("DeleteCustomer")]
    public async Task<ActionResult> DeleteCustomer([FromQuery] int customerId)
    {
        await customersRepo.DeleteAsync(customerId);
        return Ok();
    }
    
    // Orders
    [HttpPost("CreateOrder")]
    public async Task<ActionResult> CreateOrder([FromBody] Order order)
    {
        await ordersRepo.CreateAsync(order);
        return Ok();
    }
    
    [HttpGet("GetOrder")]
    public async Task<ActionResult<Order>> GetOrder([FromQuery] int orderId)
    {
        var order = await ordersRepo.ReadAsync(orderId);
        if (order is null) return NotFound();
        return Ok(order);
    }
    
    [HttpPut("UpdateOrder")]
    public async Task<ActionResult> UpdateOrder([FromBody] Order order)
    {
        await ordersRepo.UpdateAsync(order);
        return Ok();
    }
    
    [HttpDelete("DeleteOrder")]
    public async Task<ActionResult> DeleteOrder([FromQuery] int orderId)
    {
        await ordersRepo.DeleteAsync(orderId);
        return Ok();
    }
}