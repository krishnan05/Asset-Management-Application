// AssetManagement.Data/AssetManagementDbContext.cs

using Microsoft.EntityFrameworkCore;
using AssetManagement.Shared.Models; // Accesses your models

namespace AssetManagement.Data
{
    public class AssetManagementDbContext : DbContext
    {
        // 1. Define DbSets for each model
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }

        // 2. Standard Constructor
        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options)
            : base(options)
        {
        }

        // 3. Model Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Store the AssetStatus enum as a string in the database
            modelBuilder.Entity<Asset>()
                .Property(a => a.Status)
                .HasConversion<string>(); 

            // Seed initial data (optional)
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice Johnson", Email = "alice@amc.com", Department = "IT" },
                new Employee { Id = 2, Name = "Bob Smith", Email = "bob@amc.com", Department = "Sales" }
            );
        }
    }
}