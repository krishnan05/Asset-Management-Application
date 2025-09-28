using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Data.Repositories;

public class AssetAssignmentRepository : IAssetAssignmentRepository
{
    private readonly AssetManagementDbContext _context;

    public AssetAssignmentRepository(AssetManagementDbContext context)
    {
        _context = context;
    }

    // --- CORE BUSINESS LOGIC IMPLEMENTATION ---
    
    public async Task<AssetAssignment> AssignAssetAsync(AssetAssignment assignment)
    {
        // 1. Check if asset exists and is available
        var asset = await _context.Assets.FindAsync(assignment.AssetId);
        if (asset == null)
            throw new ArgumentException($"Asset with ID {assignment.AssetId} not found.");
            
        // OPTIONAL: Prevent assigning an already assigned asset.
        if (asset.Status == AssetStatus.Assigned || asset.Status == AssetStatus.InUse)
            throw new InvalidOperationException($"Asset '{asset.Name}' is already assigned or in use.");

        // 2. Update the asset status to 'Assigned'
        asset.Status = AssetStatus.Assigned; 
        _context.Assets.Update(asset);

        // 3. Add the new assignment record
        _context.AssetAssignments.Add(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<bool> ReturnAssetAsync(int assignmentId)
    {
        // Find the active assignment, including the related Asset
        var assignment = await _context.AssetAssignments
            .Include(a => a.Asset)
            .FirstOrDefaultAsync(a => a.Id == assignmentId && a.ReturnDate == null);

        if (assignment == null)
        {
            return false; // Assignment not found or already returned
        }

        // 1. Set the return date on the assignment record
        assignment.ReturnDate = DateTime.UtcNow;

        // 2. Update the asset status back to 'Available'
        if (assignment.Asset != null)
        {
            assignment.Asset.Status = AssetStatus.Available; 
            _context.Assets.Update(assignment.Asset);
        }

        _context.AssetAssignments.Update(assignment);
        await _context.SaveChangesAsync();
        return true;
    }

    // --- REPORTING / UTILITY IMPLEMENTATION ---

    public async Task<IEnumerable<AssetAssignment>> GetActiveAssignmentsAsync()
    {
        return await _context.AssetAssignments
            .Include(a => a.Asset)
            .Include(e => e.Employee)
            .Where(a => a.ReturnDate == null) // Filter for only records with no return date
            .ToListAsync();
    }
    
    public async Task<IEnumerable<AssetAssignment>> GetAssignmentsByAssetIdAsync(int assetId)
    {
        return await _context.AssetAssignments
            .Include(e => e.Employee)
            .Where(a => a.AssetId == assetId)
            .OrderByDescending(a => a.AssignedDate) // Show most recent first
            .ToListAsync();
    }

    // --- STANDARD CRUD IMPLEMENTATION (Kept for completeness) ---

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