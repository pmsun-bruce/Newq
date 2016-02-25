namespace Newq
{
    using System;

    /// <summary>
    /// The WHERE clause is used to
    /// extract only those records that fulfill a specified criterion.
    /// </summary>
    public class SelectWhereClause : WhereClause
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public SelectWhereClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// SELECT column_name, aggregate_function(column_name)
        /// FROM table_name
        /// WHERE column_name operator value
        /// GROUP BY column_name
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public GroupByClause GroupBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new GroupByClause(Statement), setTarget) as GroupByClause;
        }

        /// <summary>
        /// SELECT column_name, aggregate_function(column_name)
        /// FROM table_name
        /// WHERE column_name operator value
        /// ORDER BY column_name[ASC | DESC]
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public OrderByClause OrderBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new OrderByClause(Statement), setTarget) as OrderByClause;
        }
    }
}
