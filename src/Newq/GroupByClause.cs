namespace Newq
{
    using System;

    /// <summary>
    /// 
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
        /// 
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public HavingClause Having(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new HavingClause(Statement), setFilter) as HavingClause;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public OrderByClause OrderBy(Action<TargetColumns> setTarget)
        {
            return Provider.SetTarget(new OrderByClause(this), setTarget) as OrderByClause;
        }
    }
}
