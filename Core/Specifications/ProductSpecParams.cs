namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;


        private int _pageSize = 6;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? Sort { get; set; }


        //#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private string? _search;
        //#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? Search
        {
            get { return _search; }
            set { _search = value?.ToLower(); }
        }
    }
}