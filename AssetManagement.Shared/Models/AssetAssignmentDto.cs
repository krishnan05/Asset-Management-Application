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

        public DateTime? ReturnDate { get; set; }
        public string? AssetName { get; set; }
        public string? EmployeeName { get; set; }
    }
}
