namespace Newq
{
    /// <summary>
    /// The ORDER BY clause is used to
    /// sort the result-set by one or more columns.
    /// </summary>
    public class OrderByClause : Clause
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderByClause"/> class.
        /// </summary>
        /// <param name="statement"></param>
        public OrderByClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Target.ToString().Length > 0
                ? string.Format("ORDER BY {0} ", GetTarget())
                : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetTarget()
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
