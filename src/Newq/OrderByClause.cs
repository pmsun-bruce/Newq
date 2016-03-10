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
        public override string ToSql()
        {
            var sql = Target.GetCustomization();

            if (sql.Length > 0)
            {
                sql = string.Format("ORDER BY {0} ", sql);
            }

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string GetOrder(SortOrder order)
        {
            return order == SortOrder.Desc ? "DESC" : "ASC";
        }
    }
}
