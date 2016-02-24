namespace Newq
{
    using System;

    /// <summary>
    /// 
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
        /// 
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public GroupByClause GroupBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new GroupByClause(Statement), setTarget) as GroupByClause;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public OrderByClause OrderBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new OrderByClause(Statement), setTarget) as OrderByClause;
        }
    }
}
