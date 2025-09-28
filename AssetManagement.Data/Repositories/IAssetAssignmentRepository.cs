using AssetManagement.Shared.Models;

namespace AssetManagement.Data.Repositories;

public interface IAssetAssignmentRepository
{
    // Standard CRUD
    Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync();
    Task<AssetAssignment?> GetAssignmentByIdAsync(int id);
    Task UpdateAssignmentAsync(AssetAssignment assignment);
    Task DeleteAssignmentAsync(int id); // Note: Deleting history is usually restricted

    // --- CORE BUSINESS LOGIC METHODS ---
    
    // C: Creates an assignment and sets Asset.Status to Assigned
    Task<AssetAssignment> AssignAssetAsync(AssetAssignment assignment);

    // R: Gets only assignments where ReturnDate is NULL
    Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync();

    // U: Sets the ReturnDate and sets Asset.Status to Available
    Task<bool> ReturnAssetAsync(int assignmentId);

    // R: Get all assignments for a specific asset (useful for detail page)
    Task<IEnumerable<AssetAssignment>> GetAssignmentsByAssetIdAsync(int assetId);
}