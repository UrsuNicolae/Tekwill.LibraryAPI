namespace Library.Domain.Common
{
    public class PaginatedList<T> where T : new()
    {
        public PaginatedList(List<T> items, int pageNumber, int totalPages)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}
