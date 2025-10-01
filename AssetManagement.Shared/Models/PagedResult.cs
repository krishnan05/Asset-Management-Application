using System.Collections.Generic;

namespace AssetManagement.Shared.Models
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
