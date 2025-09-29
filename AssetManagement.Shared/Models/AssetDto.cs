using System;

namespace AssetManagement.Shared.Models
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public AssetStatus Status { get; set; }
        
    }
}