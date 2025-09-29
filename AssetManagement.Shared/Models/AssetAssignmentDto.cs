using System;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Shared.Models
{
    public class AssetAssignmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Asset is required")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Assigned date is required")]
        public DateTime AssignedDate { get; set; }

        // Nullable so it's optional
        public DateTime? ReturnDate { get; set; }

        // For display in Active Assignments grid
        public string? AssetName { get; set; }
        public string? EmployeeName { get; set; }
    }
}
