using Microsoft.EntityFrameworkCore;
using AssetManagement.Data;             // Assuming Data is directly under AssetManagement
using AssetManagement.Data.Repositories;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Configuration; 
using System; // Ensure System namespace is included for InvalidOperationException

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

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            // CRITICAL: Ensure the correct client port is trusted (was 5153 in the error log)
            policy.WithOrigins("http://localhost:5153", "http://localhost:5083") // Added 5083 for robustness
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

// ******* CRITICAL FIX: The UseCors call was missing! *******
app.UseCors(); 
// ***********************************************************

app.UseHttpsRedirection();

// 1. CRITICAL FIX: Map Controllers BEFORE Blazor Fallback
// This guarantees /api/Assets hits the controller.
app.MapControllers(); 

// 2. Map static files (Blazor WASM client's wwwroot content)
app.UseStaticFiles();

// 3. Map Blazor framework files and set fallback route LAST
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.Run();