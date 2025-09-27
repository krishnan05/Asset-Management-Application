namespace AssetManagement.Shared.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public AssetStatus Status { get; set; } = AssetStatus.Available;

    // Navigation
    public ICollection<AssetAssignment>? AssetAssignments { get; set; }
}
