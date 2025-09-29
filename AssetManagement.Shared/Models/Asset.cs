using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AssetManagement.Shared.Models;

public class Asset
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string SerialNumber { get; set; } = string.Empty;

    public AssetStatus Status { get; set; } = AssetStatus.Available;
    
    public DateTime PurchaseDate { get; set; } = DateTime.Now; 

    public ICollection<AssetAssignment>? AssetAssignments { get; set; }
}