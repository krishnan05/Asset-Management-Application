using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components; 
using Microsoft.AspNetCore.Components.Routing;
using AssetManagementApp;
using AssetManagementApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. HttpClient registration 
builder.Services.AddScoped(sp => new HttpClient 
{
    // NOTE: Replace 5083 with your actual ASP.NET Core Server's HTTPS port if different.
    BaseAddress = new Uri("http://localhost:5083/") 
});

// 2. Register your custom services
// Asset and Employee registrations (kept once)
builder.Services.AddScoped<IAssetClientService, AssetClientService>();
builder.Services.AddScoped<IEmployeeClientService, EmployeeClientService>();

// CRITICAL FIX: Asset Assignment Service Registration
builder.Services.AddScoped<IAssetAssignmentClientService, AssetAssignmentClientService>();

await builder.Build().RunAsync();