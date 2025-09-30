using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Shared.Models;

namespace AssetManagement.Data
{
    public class AssetManagementDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }

        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice Johnson", Email = "alice@amc.com", Department = "IT" },
                new Employee { Id = 2, Name = "Bob Smith", Email = "bob@amc.com", Department = "Sales" }
            );
        }
    }
}
