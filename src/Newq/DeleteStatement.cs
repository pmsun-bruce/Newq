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
        public DeleteStatement(Table table) : base(table)
        {

        }

        /// <summary>
        /// Gets or sets <see cref="TopRows"/>.
        /// </summary>
        public int TopRows { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IsPercent"/>.
        /// </summary>
        public bool IsPercent { get; set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = string.Format("DELETE {0} FROM {1} ", GetParameters(), Context[0]);

            Clauses.ForEach(clause => sql += clause.ToSql());

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetParameters()
        {
            var parameters = string.Empty;

            if (TopRows > 0)
            {
                parameters = IsPercent
                    ? string.Format("TOP({0}) PERCENT ", TopRows)
                    : string.Format("TOP({0}) ", TopRows);
            }

            return parameters;
        }

        /// <summary>
        /// DELETE
        /// FROM table_name1
        /// INNER JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <returns></returns>
        public DeleteStatement Join<T>(Action<Filter> handler)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, handler) as DeleteStatement;
        }

        /// <summary>
        /// DELETE
        /// FROM table_name1
        /// JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public DeleteStatement Join<T>(JoinType type, Action<Filter> handler)
        {
            return Provider.Join<T>(this, type, handler) as DeleteStatement;
        }

        /// <summary>
        /// DELETE
        /// FROM table_name
        /// WHERE column_name operator value
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public WhereClause Where(Action<Filter> handler)
        {
            return Provider.Filtrate(new WhereClause(this), handler);
        }
    }
}
