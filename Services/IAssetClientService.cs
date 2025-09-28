// Location: D:\AssetManagementApp\Services\IAssetClientService.cs

using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementApp.Services
{
    // This interface must be defined ONCE in this file.
    public interface IAssetClientService
    {
        Task<List<AssetDto>?> GetAssetsAsync(); 
        Task<AssetDto?> GetAssetByIdAsync(int id); 
        Task CreateAssetAsync(Asset asset); 
        Task UpdateAssetAsync(Asset asset); 
        Task DeleteAssetAsync(int id);
    }
}