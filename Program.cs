using ContactsApi.Context;
using ContactsApi.Repos;
using ContactsApi.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Contacts API", Version = "v1" });
});

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
        dbContext.Database.Migrate();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
}

try
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();
    System.Console.WriteLine("Database connection established successfully.");
}
catch (NpgsqlException ex)
{
    System.Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
}
catch (Exception ex)
{
    System.Console.WriteLine($"An unexpected error occurred: {ex.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
