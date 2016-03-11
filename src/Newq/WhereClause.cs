namespace Newq
{
    using System;

    /// <summary>
    /// The WHERE clause is used to
    /// extract only those records that fulfill a specified criterion.
    /// </summary>
    public class WhereClause : Clause
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhereClause"/> class.
        /// </summary>
        /// <param name="statement"></param>
        public WhereClause(Statement statement) : base(statement)
        {
            Filter = new Filter(statement.Context);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomizable<Action<Filter, Context>> Filter { get; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = (Filter as Filter).GetCustomization();

            if (sql.Length > 0)
            {
                sql = string.Format("WHERE {0} ", sql);
            }

            return sql;
        }
    }
}
