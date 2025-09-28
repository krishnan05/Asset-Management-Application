using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components; 
using AssetManagementApp;
using AssetManagementApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IAssetClientService, AssetClientService>(client => 
{
    client.BaseAddress = new Uri("http://localhost:5083/");
});

builder.Services.AddHttpClient<IEmployeeClientService, EmployeeClientService>(client => 
{
    client.BaseAddress = new Uri("http://localhost:5083/");
});

await builder.Build().RunAsync();