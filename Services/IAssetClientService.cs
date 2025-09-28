using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    // The name the Razor component is looking for
    public interface IAssetClientService 
    {
        // FIX: Use Asset model for all types
        Task<List<Asset>?> GetAssetsAsync(); 
        Task<Asset?> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
    }
}