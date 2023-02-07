using ItemsMicroservice.Api.Modules;
using ItemsMicroservice.Application;
using ItemsMicroservice.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom services
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add custom modules
await app.UseInfrastructureAsync();

app.UseHttpsRedirection();

// Map endpoint
app.MapItemsEndpoints();

app.Run();