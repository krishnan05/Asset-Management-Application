namespace AssetManagement.Shared.Models;

public class AssetAssignment
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnDate { get; set; }
    public Asset? Asset { get; set; }
    public Employee? Employee { get; set; }
}
