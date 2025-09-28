using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetManagementDbContext _context;

        public AssetRepository(AssetManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets
                .Include(a => a.AssetAssignments)!
                .ThenInclude(aa => aa.Employee)
                .ToListAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            return await _context.Assets
                .Include(a => a.AssetAssignments)!
                .ThenInclude(aa => aa.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAssetAsync(Asset asset)
        {
            // CRITICAL FIX: Ensure the Id is 0. 
            // This tells the database (via Entity Framework) to generate a new ID 
            // for this new record insertion.
            if (asset.Id != 0)
            {
                asset.Id = 0; 
            }
            
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status)
        {
            return await _context.Assets.Where(a => a.Status == status).ToListAsync();
        }
        
        public async Task UpdateAssetAsync(Asset asset)
        {
            // EF Core will automatically track and update based on the existing Id
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var assetToDelete = await _context.Assets.FindAsync(id);
            if (assetToDelete != null)
            {
                _context.Assets.Remove(assetToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}