namespace AssetManagement.Shared.Models
{
    public enum AssetStatus 
    { 
        // The names used here MUST match the names in AssetContext.cs
        InUse, 
        Available,
        Maintenance,
        Assigned,
        Retired
    }
}