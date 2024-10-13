using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Otus.Highload.Configurations;
using Otus.Highload.Data.Repositories;
using Otus.Highload.Extensions;
using Otus.Highload.Interfaces.Repositories;
using Otus.Highload.Interfaces.Services;
using Otus.Highload.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddSwagger();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// DB configuration
var connectionString = builder.Configuration.GetConnectionString("SocialNetwork") 
                       ?? throw new InvalidOperationException("Empty connection string");

builder.Services.AddNpgsqlDataSource(connectionString);

// BL services
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

// Add services to the container.
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

await app.RunAsync();