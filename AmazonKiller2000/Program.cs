using AmazonKiller2000;
using AmazonKiller2000.Repositories;
using AmazonKiller2000.Repositories.Redis;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Postgres DB
builder.Services.AddDbContext<PostgresDBContext>();

builder.Services.AddTransient<BookRepository>();
builder.Services.AddTransient<AuthorRepository>();

// Mongo DB
builder.Services.AddSingleton<MongoDBContext>(p => new MongoDBContext(Environment.GetEnvironmentVariable("mongodb-url")!,
    Environment.GetEnvironmentVariable("mongodb-name")!));

builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<OrderRepository>();

// Cache
builder.Services.AddSingleton(new RedisContext(Environment.GetEnvironmentVariable("redis-host")!, 6379, Environment.GetEnvironmentVariable("redis-password")!));

builder.Services.AddTransient<BookCacheRepository>();
builder.Services.AddTransient<AuthorCacheRepository>();


var app = builder.Build();

// Apply migrations from datamodel to postgres
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PostgresDBContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

// Apply mongo DB database seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MongoDBContext>();

    context.ApplyDataSeed();
}

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
