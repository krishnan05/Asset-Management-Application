using System.Collections.Generic; // Make sure this using is present
using System.Threading.Tasks; // Make sure this using is present
using AssetManagement.Shared.Models;

namespace AssetManagement.Data.Repositories
{
    public interface IAssetAssignmentRepository
    {
        Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync();
        
        // CRITICAL FIX: Add the missing method signature
        Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync(); 
        
        Task<AssetAssignment?> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(AssetAssignment assignment);
        Task UpdateAssignmentAsync(AssetAssignment assignment);
        Task DeleteAssignmentAsync(int id);
    }
}