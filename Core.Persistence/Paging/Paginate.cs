namespace Exino.Domain.Paging
{
    public class Paginate<T> : IPaginate<T>
    {
        public Paginate(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.Pagination = new Pagination()
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                TotalItems = count
            };
            this.Items = items;

        }
        public Pagination Pagination { get; set; }
        public IList<T> Items { get; set; }
    }

    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}