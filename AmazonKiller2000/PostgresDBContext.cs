using AmazonKiller2000.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKiller2000;

public class PostgresDBContext : DbContext
{
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    
    public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
    {
        
    }
    public PostgresDBContext(){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.ISBN);
            entity.Property(b => b.ISBN).IsRequired();
            entity.Property(b => b.Title).IsRequired();
            entity.Property(b => b.AuthorId).IsRequired();
            
            entity.Property(b => b.StockLevel).IsRequired();
            
        });

        modelBuilder.Entity<Book>()
            .HasOne(book => book.Author)
            .WithMany()
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id)
                .ValueGeneratedOnAdd().IsRequired();
            entity.Property(a=>a.FirstName).IsRequired();
            entity.Property(a => a.LastName).IsRequired();
        }); 

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString")!;
        optionsBuilder.UseNpgsql(connectionString);
    }
}