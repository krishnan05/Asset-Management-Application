using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetManagementDbContext _context;

        public AssetRepository(AssetManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync() =>
            await _context.Assets.Include(a => a.AssetAssignments).ThenInclude(aa => aa.Employee).ToListAsync();

        public async Task<Asset?> GetAssetByIdAsync(int id) =>
            await _context.Assets.Include(a => a.AssetAssignments).ThenInclude(aa => aa.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssetAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var entity = await _context.Assets.FindAsync(id);
            if (entity != null)
            {
                _context.Assets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status) =>
            await _context.Assets.Where(a => a.Status == status).ToListAsync();

        // NEW methods
        public async Task<PagedResult<Asset>> GetFilteredAssetsAsync(AssetFilterParams filters)
        {
            var query = _context.Assets.AsQueryable();

            if (!string.IsNullOrEmpty(filters.SerialNumber))
                query = query.Where(a => a.SerialNumber.Contains(filters.SerialNumber));

            if (filters.Status.HasValue)
                query = query.Where(a => a.Status == filters.Status.Value);

            if (!string.IsNullOrEmpty(filters.AssetType))
                query = query.Where(a => a.AssetType == filters.AssetType);

            var total = await query.CountAsync();
            var items = await query
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

            return new PagedResult<Asset> { Items = items, TotalCount = total };
        }

        public async Task<AssetSummaryDto> GetAssetsSummaryAsync()
{
    return new AssetSummaryDto
    {
        TotalAssets = await _context.Assets.CountAsync(),
        AssignedAssets = await _context.Assets.CountAsync(a => a.Status == AssetStatus.Assigned),
        AvailableAssets = await _context.Assets.CountAsync(a => a.Status == AssetStatus.Available),
        UnderRepairAssets = await _context.Assets.CountAsync(a => a.Status == AssetStatus.UnderRepair),
        RetiredAssets = await _context.Assets.CountAsync(a => a.Status == AssetStatus.Retired),
        SpareAssets = await _context.Assets.CountAsync(a => a.Status == AssetStatus.Spare) // ✅ FIX
    };
}

        public async Task<List<Asset>> GetAssetsForExportAsync(AssetFilterParams filters)
        {
            var result = await GetFilteredAssetsAsync(filters);
            return result.Items.ToList();
        }
    }
}
