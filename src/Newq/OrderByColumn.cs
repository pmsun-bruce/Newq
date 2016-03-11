namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderByColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderByColumn"/> class.
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Column, OrderByClause.GetOrder(Order));
        }
    }
}
