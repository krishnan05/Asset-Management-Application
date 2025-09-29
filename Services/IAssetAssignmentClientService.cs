using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagement.Shared.Models;

namespace AssetManagementApp.Services
{
    public interface IAssetAssignmentClientService
    {
        Task<List<AssetAssignmentDto>?> GetAllAssignmentsAsync(); 

        Task<List<AssetAssignmentDto>?> GetActiveAssignmentsAsync();
        Task<AssetAssignmentDto?> GetAssignmentByIdAsync(int id);
        Task AssignAssetAsync(AssetAssignmentDto assignment);
        Task ReturnAssetAsync(int id);
    }
}