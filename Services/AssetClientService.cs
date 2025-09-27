// AssetManagementApp/Services/AssetClientService.cs

using AssetManagement.Shared.Models;
using System.Net.Http.Json;

namespace AssetManagementApp.Services
{
    public class AssetClientService : IAssetClientService
    {
        private readonly HttpClient _httpClient;

        public AssetClientService(IHttpClientFactory httpClientFactory)
        {
            // Retrieve the named client configured in Program.cs
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

        public async Task<List<Asset>> GetAssetsAsync()
        {
            // Maps directly to GET /api/Assets
            var assets = await _httpClient.GetFromJsonAsync<List<Asset>>("api/Assets");
            return assets ?? new List<Asset>();
        }
        
        public async Task AddAssetAsync(Asset asset)
        {
            // Maps directly to POST /api/Assets
            var response = await _httpClient.PostAsJsonAsync("api/Assets", asset);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAssetAsync(Asset asset)
        {
            // Maps directly to PUT /api/Assets/{id}
            var response = await _httpClient.PutAsJsonAsync($"api/Assets/{asset.Id}", asset);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAssetAsync(int id)
        {
            // Maps directly to DELETE /api/Assets/{id}
            var response = await _httpClient.DeleteAsync($"api/Assets/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}