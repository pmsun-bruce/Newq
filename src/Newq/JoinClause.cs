namespace Newq
{
    /// <summary>
    /// The JOIN clause is used to
    /// combine rows from two or more tables,
    /// based on a common field between them.
    /// </summary>
    public class JoinClause : Clause
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinClause"/> class.
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="table"></param>
        /// <param name="type"></param>
        public JoinClause(Statement statement, DbTable table, JoinType type = JoinType.InnerJoin) : base(statement)
        {
            JoinTable = table;
            JoinType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbTable JoinTable { get; }

        /// <summary>
        /// 
        /// </summary>
        public JoinType JoinType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetJoinType()
        {
            var type = string.Empty;

            switch (JoinType)
            {
                case JoinType.LeftJoin:
                    type = "LEFT JOIN";
                    break;
                case JoinType.RightJoin:
                    type = "RIGHT JOIN";
                    break;
                case JoinType.InnerJoin:
                    type = "INNER JOIN";
                    break;
                case JoinType.CrossJoin:
                    type = "CROSS JOIN";
                    break;
                case JoinType.LeftOuterJoin:
                    type = "LEFT OUTER JOIN";
                    break;
                case JoinType.RightOuterJoin:
                    type = "RIGHT OUTER JOIN";
                    break;
                case JoinType.FullOuterJoin:
                    type = "FULL OUTER JOIN";
                    break;
                default:
                    break;
            }

            return type;
        }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Filter.ToString().Length > 0
                ? string.Format("{0} {1} ON {2} ", GetJoinType(), JoinTable, Filter)
                : string.Empty;
        }
    }
}
