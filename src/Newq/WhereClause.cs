namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class WhereClause : Clause
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public WhereClause(Statement statement) : base(statement)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("WHERE {0} ", Filter);
        }
    }
}
