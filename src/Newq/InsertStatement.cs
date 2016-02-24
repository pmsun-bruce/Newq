namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertStatement : Statement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public InsertStatement(DbTable table) : base(table)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var columns = string.Empty;
            var values = string.Empty;

            foreach (var col in DbContext[0].Columns)
            {
                columns += string.Format("{0}, ", col);
                values += string.Format("{0}, ", col.Value.ToSqlValue());
            }

            var valueClause = string.Format("({0}) VALUES ({1})",
                columns.Remove(columns.Length - 2),
                values.Remove(values.Length - 2));

            return string.Format("INSERT INTO {0} {1} ", DbContext[0], valueClause);
        }
    }
}
