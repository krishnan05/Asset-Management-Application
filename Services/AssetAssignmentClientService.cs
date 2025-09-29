using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AssetManagement.Shared.Models;

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

        public async Task<List<AssetAssignmentDto>?> GetActiveAssignmentsAsync()
        {
            // FIX: Targets server route [HttpGet("Active")]
            return await _httpClient.GetFromJsonAsync<List<AssetAssignmentDto>>($"{BaseApiUrl}/Active"); 
        }

        public async Task<AssetAssignmentDto?> GetAssignmentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AssetAssignmentDto>($"{BaseApiUrl}/{id}");
        }

        public async Task AssignAssetAsync(AssetAssignmentDto assignment)
        {
            // FIX: Targets server route [HttpPost("Assign")] (This fixes the 405 error)
            var response = await _httpClient.PostAsJsonAsync($"{BaseApiUrl}/Assign", assignment); 
            response.EnsureSuccessStatusCode();
        }

        public async Task ReturnAssetAsync(int id)
        {
            // FIX: Targets server route [HttpPut("Return/{assignmentId}")]
            var response = await _httpClient.PutAsync($"{BaseApiUrl}/Return/{id}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}