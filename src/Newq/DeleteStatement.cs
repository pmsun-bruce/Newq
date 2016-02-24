namespace Newq
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class DeleteStatement : Statement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public DeleteStatement(DbTable table) : base(table)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public int TopNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("DELETE {0}{1} FROM {1} ", GetParameters(), DbContext[0]);
        }

        private string GetParameters()
        {
            var topClause = string.Empty;

            if (TopNumber > 0)
            {
                topClause = IsPercent
                    ? string.Format("TOP({0}) PERCENT ", TopNumber)
                    : string.Format("TOP({0}) ", TopNumber);
            }

            return topClause;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public DeleteStatement Join<T>(Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, setFilter) as DeleteStatement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public DeleteStatement Join<T>(JoinType type, Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, type, setFilter) as DeleteStatement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public WhereClause Where(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new WhereClause(this), setFilter) as WhereClause;
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
