namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class Paginator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paginator"/> class.
        /// </summary>
        public Paginator()
        {
            TotalRows = 0;
            PageSize = 10;
            CurrentPage = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages
        {
            get
            {
                var hasFraction = TotalRows % PageSize == 0;
                var total = TotalRows / PageSize;

                return hasFraction ? total + 1 : total;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BeginRowNumber
        {
            get { return (CurrentPage - 1) * PageSize; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int EndRowNumber
        {
            get { return CurrentPage * PageSize; }
        }
    }
}
