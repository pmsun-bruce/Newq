namespace Newq
{
    /// <summary>
    /// The WHERE clause is used to
    /// extract only those records that fulfill a specified criterion.
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
