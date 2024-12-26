using Shopping.API.Data;
using Shopping.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Configure MongoDB
var mongoSettings = new MongoDbSettings 
{ 
    ConnectionString = builder.Configuration["MongoDBConnectionString"] ?? "mongodb://localhost:27017"
};
builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<ProductContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();