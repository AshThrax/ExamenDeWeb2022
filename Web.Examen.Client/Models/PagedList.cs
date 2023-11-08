namespace Web.Examen.Client.Models
{
    public class PagedList<T>:List<T>
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int PageNumber { get; set; }
        public List<T> Items { get; set; }

        public PagedList(List<T> items, int Count, int pageNumber, int pageSize)
        {
            TotalCount = Count;
            TotalPage = (int)Math.Ceiling(Count / (double)pageSize);
            PageNumber = pageNumber;
            Items = items;
        }

        public bool HasNext => PageNumber > 1;
        public bool HasPrev => PageNumber < TotalPage;

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pagesize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
            var page= new PagedList<T>(items, count, pageNumber, pagesize);
            return page;
        }
    }
}
