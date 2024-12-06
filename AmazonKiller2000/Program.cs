using AmazonKiller2000;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDBContext>(p => new MongoDBContext(Environment.GetEnvironmentVariable("mongodb-url")!,
    Environment.GetEnvironmentVariable("mongodb-name")!));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
