// Location: D:\AssetManagementApp\Services\AssetAssignmentClientService.cs

using AssetManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public class AssetAssignmentClientService : IAssetAssignmentClientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "api/AssetAssignments";

        public AssetAssignmentClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AssetAssignment>?> GetActiveAssignmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AssetAssignment>>($"{BaseApiUrl}/Active");
        }

        public async Task<List<AssetAssignment>?> GetAssignmentsByAssetIdAsync(int assetId)
        {
            return await _httpClient.GetFromJsonAsync<List<AssetAssignment>>($"{BaseApiUrl}/ByAsset/{assetId}");
        }

        public async Task AssignAssetAsync(AssetAssignment assignment)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}/Assign", assignment);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task ReturnAssetAsync(int assignmentId)
        {
            // PUT calls often don't include a body, passing null content
            var response = await _httpClient.PutAsync($"{BaseApiUrl}/Return/{assignmentId}", content: null);
            response.EnsureSuccessStatusCode();
        }
    }
}