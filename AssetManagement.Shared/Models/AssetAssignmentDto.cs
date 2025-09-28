using System;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Shared.Models
{
    public class AssetAssignmentDto
    {
        public int Id { get; set; }

        [Required]
        public int AssetId { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }
        
        [Required]
        public DateTime AssignedDate { get; set; }
        
        // **FIXED:** Using 'ReturnDate' to match your entity
        public DateTime? ReturnDate { get; set; } 

        // Properties for displaying names on the client
        public string AssetName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }
}