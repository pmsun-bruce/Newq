namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderByColumn
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="order"></param>
        public OrderByColumn(Column column, SortOrder order)
        {
            Column = column;
            Order = order;
        }

        /// <summary>
        /// 
        /// </summary>
        public Column Column { get; }

        /// <summary>
        /// 
        /// </summary>
        public SortOrder Order { get; }
    }
}
