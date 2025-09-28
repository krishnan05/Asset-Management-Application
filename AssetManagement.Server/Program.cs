using Microsoft.EntityFrameworkCore;
using AssetManagement.Data;
using AssetManagement.Data.Repositories;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Configuration; 
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AssetManagementDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAssetAssignmentRepository, AssetAssignmentRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
            {
                policy.WithOrigins("http://localhost:5153")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(); 

app.UseStaticFiles();

app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.Run();