using HishabNikash.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddControllers().AddApplicationPart(typeof(HishabNikash.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Debug.WriteLine($"Connection String => {builder.Configuration.GetConnectionString("sqlConnection")}");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
    dbContext.Database.Migrate();  // Apply any pending migrations
}

app.Run();
