namespace Model.Filtering
{
    public class PaginationArgs
    {
        public int PageNumber { get; set; }

        public int CountPerPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public PaginationArgs()
        {
            PageNumber = 1;
            TotalPages = 1;
            CountPerPage = 10;
        }
    }
}
