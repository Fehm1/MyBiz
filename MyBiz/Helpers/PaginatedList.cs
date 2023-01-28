namespace MyBiz.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> values, int count, int pageSize, int activePage)
        {
            AddRange(values);
            ActivePage = activePage;
            PageSize = pageSize;
            TotalPageCount = (int)Math.Ceiling((double)count / pageSize);
        }

        public int ActivePage { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious { get => ActivePage > 1; }
        public bool HasNext { get => ActivePage < TotalPageCount; }

        public static PaginatedList<T> Create(IQueryable<T> query, int pageSize, int activePage)
        {
            return new PaginatedList<T>(query.Skip((activePage - 1) * pageSize).Take(pageSize).ToList(), query.Count(), pageSize, activePage);
        }
    }

}
