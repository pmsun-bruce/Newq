namespace Newq
{
    using System;

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
            Target = new Target(statement.Context);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomizable<Action<Target>> Target { get; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetTarget().Length > 0
                ? string.Format("ORDER BY {0} ", GetTarget())
                : string.Empty;
        }

        /// <summary>
        /// Returns a string that represents the current clause target.
        /// </summary>
        /// <returns></returns>
        protected string GetTarget()
        {
            var target = string.Empty;
            var items = ((Target)Target).GetOrderByColumns();

            foreach (var item in items)
            {
                target += string.Format("{0} {1}, ", item.Column, GetOrder(item.Order));
            }

            return target.Length > 0 ? target.Remove(target.Length - 2) : string.Empty;
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
