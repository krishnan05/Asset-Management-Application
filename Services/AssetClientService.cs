// Location: D:\AssetManagementApp\Services\AssetClientService.cs

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

        public async Task<List<AssetDto>?> GetAssetsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AssetDto>>(BaseApiUrl);
        }

        public async Task<AssetDto?> GetAssetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AssetDto>($"{BaseApiUrl}/{id}");
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