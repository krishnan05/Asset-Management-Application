using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    public interface IAssetClientService
    {
        Task<List<Asset>?> GetAllAssetsAsync(); 
        
        Task<Asset?> GetAssetByIdAsync(int id);
        Task CreateAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
    }
}