using AssetManagement.Shared.Models;

namespace AssetManagement.Data.Repositories
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(int id);
        Task AddAssetAsync(Asset asset);
        Task UpdateAssetAsync(Asset asset);
        Task DeleteAssetAsync(int id);
       
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);
    }
}