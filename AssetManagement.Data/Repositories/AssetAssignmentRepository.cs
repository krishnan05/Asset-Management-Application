using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Data.Repositories
{
    public class AssetAssignmentRepository : IAssetAssignmentRepository
    {
        private readonly AssetManagementDbContext _context;

        public AssetAssignmentRepository(AssetManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AssetAssignment>> GetAllAssignmentsAsync()
        {
            return await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(e => e.Employee)
                .ToListAsync();
        }

        public async Task<AssetAssignment?> GetAssignmentByIdAsync(int id)
        {
            return await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAssignmentAsync(AssetAssignment assignment)
        {
            _context.AssetAssignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssignmentAsync(AssetAssignment assignment)
        {
            _context.AssetAssignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssignmentAsync(int id)
        {
            var assignment = await _context.AssetAssignments.FindAsync(id);
            if (assignment != null)
            {
                _context.AssetAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
