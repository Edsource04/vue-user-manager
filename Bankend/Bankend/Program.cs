using Core.Context;
using Core.Interface;
using Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserManagerContext>(options =>
{
    options.UseSqlServer(@"Server=MDA-305;Database=UserManagerDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
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

builder.Services.AddControllers();
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
