namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class Paginator
    {
        /// <summary>
        /// 
        /// </summary>
        public Paginator()
        {
            TotalRows = 1;
            TotalPages = 1;
            PageSize = 10;
            CurrentRowNumber = 1;
            CurrentPage = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalRows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CurrentRowNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CurrentPage { get; set; }
    }
}
