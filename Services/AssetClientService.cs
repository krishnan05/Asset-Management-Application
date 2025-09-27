using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public class AssetClientService : IAssetClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "api/Assets";

        public AssetClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Return type matches interface: Task<List<Asset>?>
        public async Task<List<Asset>?> GetAssetsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Asset>>(BaseApiUrl);
        }

        // Implementation for interface member
        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Asset>($"{BaseApiUrl}/{id}");
        }
        
        public async Task CreateAssetAsync(Asset asset)
{
    var response = await _httpClient.PostAsJsonAsync(BaseApiUrl, asset);
    response.EnsureSuccessStatusCode();
}

        public async Task UpdateAssetAsync(Asset asset)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseApiUrl}/{asset.Id}", asset);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}