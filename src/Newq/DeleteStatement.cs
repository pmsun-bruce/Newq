namespace Newq
{
    using System;

    /// <summary>
    /// The DELETE statement is used to
    /// delete rows in a table.
    /// </summary>
    public class DeleteStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStatement"/> class.
        /// </summary>
        /// <param name="table"></param>
        public DeleteStatement(DbTable table) : base(table)
        {

        }

        /// <summary>
        /// Gets or sets <see cref="TopNumber"/>.
        /// </summary>
        public int TopNumber { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IsPercent"/>.
        /// </summary>
        public bool IsPercent { get; set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("DELETE {0} FROM {1} ", GetParameters(), DbContext[0]);
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
        /// DELETE
        /// FROM table_name1
        /// INNER JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public DeleteStatement Join<T>(Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, setFilter) as DeleteStatement;
        }

        /// <summary>
        /// DELETE
        /// FROM table_name1
        /// JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
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
        /// DELETE
        /// FROM table_name
        /// WHERE column_name operator value
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public WhereClause Where(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new WhereClause(this), setFilter) as WhereClause;
        }
    }
}
