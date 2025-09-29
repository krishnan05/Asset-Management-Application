using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components; 
using Microsoft.AspNetCore.Components.Routing;
using AssetManagementApp;
using AssetManagementApp.Services; 

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri("http://localhost:5083/") 
});

builder.Services.AddScoped<IAssetClientService, AssetClientService>();

builder.Services.AddScoped<IEmployeeClientService, EmployeeClientService>();
builder.Services.AddScoped<IAssetAssignmentClientService, AssetAssignmentClientService>();


await builder.Build().RunAsync();