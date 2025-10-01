namespace AssetManagement.Shared.Models
{
    public class AssetFilterParams
    {
        public string? AssetType { get; set; }
        public AssetStatus? Status { get; set; }
        public string? SerialNumber { get; set; }
        public int? AssignedEmployeeId { get; set; } // optional
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "Name"; // default
        public string SortDir { get; set; } = "asc"; // asc or desc
    }
}
