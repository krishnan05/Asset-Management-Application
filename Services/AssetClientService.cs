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

        public async Task<List<Asset>?> GetAllAssetsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Asset>>(BaseApiUrl);

        public async Task<Asset?> GetAssetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Asset>($"{BaseApiUrl}/{id}");

        public async Task CreateAssetAsync(Asset asset) =>
            (await _httpClient.PostAsJsonAsync(BaseApiUrl, asset)).EnsureSuccessStatusCode();

        public async Task UpdateAssetAsync(Asset asset) =>
            (await _httpClient.PutAsJsonAsync($"{BaseApiUrl}/{asset.Id}", asset)).EnsureSuccessStatusCode();

        public async Task DeleteAssetAsync(int id) =>
            (await _httpClient.DeleteAsync($"{BaseApiUrl}/{id}")).EnsureSuccessStatusCode();

        public async Task<AssetSummaryDto?> GetDashboardDataAsync() =>
            await _httpClient.GetFromJsonAsync<AssetSummaryDto>($"{BaseApiUrl}/summary");

        public async Task<PagedResult<Asset>?> GetFilteredAssetsAsync(AssetFilterParams filters)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}/filter", filters);
            return await response.Content.ReadFromJsonAsync<PagedResult<Asset>>();
        }

        public async Task<List<Asset>?> GetAssetsForExportAsync(AssetFilterParams filters)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}/export", filters);
            return await response.Content.ReadFromJsonAsync<List<Asset>>();
        }

        public async Task<List<Asset>?> SearchAssetsAsync(string? type, AssetStatus? status, string? serialNumber, string? employeeName)
        {
            var url = $"{BaseApiUrl}/search?";

            if (!string.IsNullOrEmpty(type)) url += $"type={type}&";
            if (status.HasValue) url += $"status={status.Value}&";
            if (!string.IsNullOrEmpty(serialNumber)) url += $"serialNumber={serialNumber}&";
            if (!string.IsNullOrEmpty(employeeName)) url += $"employeeName={employeeName}&";

            return await _httpClient.GetFromJsonAsync<List<Asset>>(url.TrimEnd('&', '?'));
        }
    }
}
