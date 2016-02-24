namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Clause : Statement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        protected Clause(Statement statement)
        {
            Statement = statement;
            DbContext = statement.DbContext;
            Target = new TargetColumns(DbContext);
            Filter = new Filter(DbContext);
        }

        /// <summary>
        /// 
        /// </summary>
        public Statement Statement { get; }

        public Filter Filter { get; protected set; }

        public override string ToSql()
        {
            return ToString();
        }
    }
}
