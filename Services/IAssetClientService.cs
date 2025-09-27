// AssetManagementApp/Services/IAssetClientService.cs

using AssetManagement.Shared.Models;

namespace AssetManagementApp.Services
{
    public interface IAssetClientService
    {
        Task<List<Asset>> GetAssetsAsync();
        // Add methods for CRUD operations corresponding to your API Controller
        Task AddAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
    }
}