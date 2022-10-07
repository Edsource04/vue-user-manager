using Core.Context;
using Core.Interface;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json");

var config = configuration.Build();
var connectionString = config.GetConnectionString("UserManagerDatabase");

builder.Services.AddDbContext<UserManagerContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IPermissionService, PermissionService>();

var vueClientOrigins = "VueClientOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: vueClientOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin();
    });
});

builder.Services.AddControllers().AddJsonOptions(jo => jo.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(vueClientOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
