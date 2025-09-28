using AssetManagement.Shared.Models; // CRITICAL: Use the model from the Shared project for the signature
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Data.Repositories
{
    public interface IAssetAssignmentRepository
    {
        Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync();
        Task<AssetAssignment?> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(AssetAssignment assignment);
        Task UpdateAssignmentAsync(AssetAssignment assignment);
        Task DeleteAssignmentAsync(int id);
        
        // FIX: Add the missing method definition
        Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync();
    }
}