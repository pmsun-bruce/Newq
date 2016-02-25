namespace Newq
{
    /// <summary>
    /// The ORDER BY clause is used to
    /// sort the result-set by one or more columns.
    /// </summary>
    public class OrderByClause : Clause
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public OrderByClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("ORDER BY {0} ", GetTarget());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetTarget()
        {
            var target = string.Empty;
            var items = Target.GetTargetColumns();

            foreach (var item in items)
            {
                target += string.Format("{0} {1}, ", item.Key, GetOrder(item.Value));
            }

            return target.Remove(target.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string GetOrder(SortOrder order)
        {
            return order == SortOrder.Desc ? "DESC" : "ASC";
        }
    }
}
