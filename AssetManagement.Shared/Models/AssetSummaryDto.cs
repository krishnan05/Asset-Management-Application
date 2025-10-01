using System;

namespace AssetManagement.Shared.Models
{
    public class AssetSummaryDto
    {
        public int TotalAssets { get; set; }
        public int AssignedAssets { get; set; }
        public int AvailableAssets { get; set; }
        public int UnderRepairAssets { get; set; }
        public int RetiredAssets { get; set; }
        public int SpareAssets { get; set; }
    }
}
