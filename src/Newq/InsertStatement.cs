namespace Newq
{
    using Newq.Extensions;

    /// <summary>
    /// The INSERT INTO statement is used to
    /// insert new records in a table.
    /// </summary>
    public class InsertStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertStatement"/> class.
        /// </summary>
        /// <param name="table"></param>
        public InsertStatement(Table table) : base(table)
        {

        }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var columns = string.Empty;
            var values = string.Empty;

            foreach (var col in Context[0].Columns)
            {
                columns += string.Format("{0}, ", col);
                values += string.Format("{0}, ", col.Value.ToSqlValue());
            }

            var valueClause = string.Format("({0}) VALUES ({1})",
                columns.Remove(columns.Length - 2),
                values.Remove(values.Length - 2));

            return string.Format("INSERT INTO {0} {1} ", Context[0], valueClause);
        }
    }
}
