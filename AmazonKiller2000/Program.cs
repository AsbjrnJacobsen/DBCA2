using AmazonKiller2000;
using AmazonKiller2000.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDBContext>(p => new MongoDBContext(Environment.GetEnvironmentVariable("mongodb-url")!,
    Environment.GetEnvironmentVariable("mongodb-name")!));

builder.Services.AddTransient<BookRepository>();
builder.Services.AddTransient<AuthorRepository>();
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<OrderRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
