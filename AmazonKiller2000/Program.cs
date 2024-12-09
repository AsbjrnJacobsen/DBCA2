using AmazonKiller2000;
using AmazonKiller2000.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<PostgresDBContext>();
builder.Services.AddSingleton<MongoDBContext>(p => new MongoDBContext(Environment.GetEnvironmentVariable("mongodb-url")!,
    Environment.GetEnvironmentVariable("mongodb-name")!));

builder.Services.AddTransient<BookRepository>();
builder.Services.AddTransient<AuthorRepository>();
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<OrderRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PostgresDBContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
