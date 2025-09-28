// Location: AssetManagement.Shared/Models/AssetDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Shared.Models
{
    // DTO for safely transferring Asset data without navigation properties
    public class AssetDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Asset Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Serial Number is required.")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase Date is required.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = string.Empty; // Holds the string representation of AssetStatus
    }
}