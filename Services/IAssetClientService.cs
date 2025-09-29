using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public interface IAssetClientService
    {
        // FIX: Ensure this method exists and is named this way
        Task<List<Asset>?> GetAllAssetsAsync(); 
        
        Task<Asset?> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
    }
}