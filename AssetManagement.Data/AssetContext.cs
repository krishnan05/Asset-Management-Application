using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
// public enum AssetStatus { InUse, Available }
namespace AssetManagement.Data
{
    // NOTE: If your actual DbContext name is AssetManagementDbContext, change the class name here.
    public class AssetContext : DbContext
    {
        // DbSet maps your Asset model to a table in the database
        public DbSet<Asset> Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FIX: Replaced string literals with AssetStatus enum members.
            modelBuilder.Entity<Asset>().HasData(
                new Asset 
                { 
                    Id = 1, 
                    Name = "Laptop", 
                    SerialNumber = "SN-LPT-001", 
                    // CRITICAL FIX: Use the enum type
                    Status = AssetManagement.Shared.Models.AssetStatus.InUse
                },
                new Asset 
                { 
                    Id = 2, 
                    Name = "Monitor", 
                    SerialNumber = "SN-MON-002", 
                    // CRITICAL FIX: Use the enum type
                     Status = AssetManagement.Shared.Models.AssetStatus.Available
                }
            );
        }
    }
}