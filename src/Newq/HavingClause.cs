namespace Newq
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class HavingClause : Clause
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public HavingClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Having {0} ", Filter);
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

