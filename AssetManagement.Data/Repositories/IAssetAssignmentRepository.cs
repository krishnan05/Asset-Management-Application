using AssetManagement.Shared.Models;

namespace AssetManagement.Data.Repositories
{
    public interface IAssetAssignmentRepository
    {
        Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync();
        Task<AssetAssignment?> GetAssignmentByIdAsync(int id);
        Task AddAssignmentAsync(AssetAssignment assignment);
        Task UpdateAssignmentAsync(AssetAssignment assignment);
        Task DeleteAssignmentAsync(int id);
    }
}
