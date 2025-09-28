// Location: AssetManagement.Shared/Models/Asset.cs

using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AssetManagement.Shared.Models
{
    // Assuming AssetStatus is an enum defined elsewhere (e.g., in a Status.cs file)
    // public enum AssetStatus { Available, Assigned, Retired } 

    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; } = string.Empty;

        // Ensure this property name/type is correct relative to your database
        public AssetStatus Status { get; set; } = AssetStatus.Available; 
        
        public DateTime PurchaseDate { get; set; } = DateTime.Now; 

        // Navigation property (Causes the JSON cycle)
        public ICollection<AssetAssignment>? AssetAssignments { get; set; } 
    }
}