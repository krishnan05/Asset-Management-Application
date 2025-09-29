using System.Collections.Generic; 
using System.Threading.Tasks; 
using AssetManagement.Shared.Models;

namespace AssetManagement.Data.Repositories
{
    public interface IAssetAssignmentRepository
    {
        Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync();
        Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync(); 
        
        Task<AssetAssignment?> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(AssetAssignment assignment);
        Task UpdateAssignmentAsync(AssetAssignment assignment);
        Task DeleteAssignmentAsync(int id);
    }
}