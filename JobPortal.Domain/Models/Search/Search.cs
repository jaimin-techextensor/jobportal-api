namespace JobPortal.Domain.Models.Search
{
    public class Search
    {
        const int maxPageSize = 50;
        
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public string? SearchText { get; set; }
        
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string? SortBy { get; set; }

        public int SortOrder { get; set; }
    }
}
