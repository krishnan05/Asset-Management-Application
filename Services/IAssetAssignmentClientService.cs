// Location: D:\AssetManagementApp\Services\IAssetAssignmentClientService.cs

using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public interface IAssetAssignmentClientService
    {
        // R: Gets only active assignments (ReturnDate is null)
        Task<List<AssetAssignment>?> GetActiveAssignmentsAsync();
        
        // R: Gets all assignments for a specific asset (for the asset details page)
        Task<List<AssetAssignment>?> GetAssignmentsByAssetIdAsync(int assetId);

        // C: Creates a new assignment (POST api/AssetAssignments/Assign)
        Task AssignAssetAsync(AssetAssignment assignment);

        // U: Completes an assignment (PUT api/AssetAssignments/Return/{id})
        Task ReturnAssetAsync(int assignmentId);
    }
}