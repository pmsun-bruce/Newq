namespace Newq
{
    using System;

    /// <summary>
    /// The GROUP BY clause is used in conjunction with
    /// the aggregate functions to group the result-set by one or more columns.
    /// </summary>
    public class GroupByClause : Clause
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public GroupByClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("GROUP BY {0} ", Target);
        }

        /// <summary>
        /// SELECT column_name, aggregate_function(column_name)
        /// FROM table_name
        /// WHERE column_name operator value
        /// GROUP BY column_name
        /// HAVING aggregate_function(column_name) operator value
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public HavingClause Having(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new HavingClause(Statement), setFilter) as HavingClause;
        }

        /// <summary>
        /// SELECT column_name, aggregate_function(column_name)
        /// FROM table_name
        /// WHERE column_name operator value
        /// GROUP BY column_name
        /// ORDER BY column_name [ASC|DESC]
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public OrderByClause OrderBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new OrderByClause(this), setTarget) as OrderByClause;
        }
    }
}
