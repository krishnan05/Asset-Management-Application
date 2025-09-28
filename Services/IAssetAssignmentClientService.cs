using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public interface IAssetAssignmentClientService
    {
        // FIX: Removed Task<List<AssetDto>?> GetAssetsAsync();
        
        // This is the correct method for fetching active ASSIGNMENTS
        Task<List<AssetAssignmentDto>?> GetActiveAssignmentsAsync();
        
        // Input remains the entity/model for simplicity on the client side
        Task AssignAssetAsync(AssetAssignment assignment);
        Task ReturnAssetAsync(int assignmentId);
    }
}