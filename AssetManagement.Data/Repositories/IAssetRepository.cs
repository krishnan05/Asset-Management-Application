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
        Task<PagedResult<Asset>> GetFilteredAssetsAsync(AssetFilterParams filters);
        Task<AssetSummaryDto> GetAssetsSummaryAsync();
        Task<List<Asset>> GetAssetsForExportAsync(AssetFilterParams filters);
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);
    }
}