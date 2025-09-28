using AssetManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    // CRITICAL FIX: Ensure the class name and interface name are spelled correctly
    public class AssetAssignmentClientService : IAssetAssignmentClientService 
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "api/AssetAssignments";

        public AssetAssignmentClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AssetAssignmentDto>?> GetActiveAssignmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AssetAssignmentDto>>($"{BaseApiUrl}/Active");
        }

        public async Task AssignAssetAsync(AssetAssignment assignment)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}/Assign", assignment);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task ReturnAssetAsync(int assignmentId)
        {
            var response = await _httpClient.PutAsync($"{BaseApiUrl}/Return/{assignmentId}", content: null);
            response.EnsureSuccessStatusCode();
        }
    }
}