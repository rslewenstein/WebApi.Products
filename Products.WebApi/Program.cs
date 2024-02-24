using AutoMapper;
using Products.Application;
using Products.Application.Interfaces;
using Products.Infrastructure.Data;
using Products.Infrastructure.Data.Interfaces;
using Products.Infrastructure.Helper;
using Products.Infrastructure.Messaging;
using Products.Infrastructure.Messaging.Interfaces;
using Products.Infrastructure.Repository;
using Products.Infrastructure.Repository.Interfaces;
using Products.Infrastructure.Worker;

var builder = WebApplication.CreateBuilder(args);

// Logger config
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Register the interfaces
builder.Services.AddScoped<ProductContext>();
builder.Services.AddSingleton<ICreateConnection, CreateConnection>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IMessageConnection, MessageConnection>();

// Calling worker
builder.Services.AddHostedService<ProductWorker>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile(new MapperProfile());
});

// Habilitando CORS
builder.Services.AddCors();

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);        

var app = builder.Build();

// Setup Dapper
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
    await context.Init();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitando CORS 
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
