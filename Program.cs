using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AssetManagementApp;
using AssetManagementApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using System.Net.Http;

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

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
