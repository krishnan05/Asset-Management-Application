using AssetManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace AssetManagement.Data
{
    public class AssetContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().HasData(
                new Asset 
                { 
                    Id = 1, 
                    Name = "Laptop", 
                    SerialNumber = "SN-LPT-001", 
                    Status = AssetManagement.Shared.Models.AssetStatus.InUse
                },
                new Asset 
                { 
                    Id = 2, 
                    Name = "Monitor", 
                    SerialNumber = "SN-MON-002", 
                     Status = AssetManagement.Shared.Models.AssetStatus.Available
                }
            );
        }
    }
}