namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Clause : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clause"/> class.
        /// </summary>
        /// <param name="statement"></param>
        protected Clause(Statement statement)
        {
            Statement = statement;
            Context = statement.Context;
        }

        /// <summary>
        /// Gets <see cref="Statement"/>.
        /// </summary>
        public Statement Statement { get; }


        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            return ToString();
        }
    }
}
