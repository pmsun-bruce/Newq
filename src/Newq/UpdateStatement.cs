namespace Newq
{
    using System;

    /// <summary>
    /// The UPDATE statement is used to
    /// update existing records in a table.
    /// </summary>
    public class UpdateStatement : Statement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        public UpdateStatement(DbTable table) : base(table)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var setClause = string.Empty;

            foreach (var col in DbContext[0].Columns)
            {
                setClause += string.Format("{0} = {1}, ", col, col.Value.ToSqlValue());
            }

            setClause = setClause.Remove(setClause.Length - 2);

            return string.Format("UPDATE {0} SET {1} ", DbContext[0], setClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = this.ToString();

            if (Clauses.Count > 0)
            {
                var isFirstJoinClause = true;

                foreach (var cls in Clauses)
                {
                    if (cls is JoinClause && isFirstJoinClause)
                    {
                        sql += string.Format("FROM {0} ", DbContext[0]);
                        isFirstJoinClause = false;
                    }

                    sql += cls.ToSql();
                }
            }

            return sql;
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// FROM table_name
        /// INNER JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public UpdateStatement Join<T>(Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, setFilter) as UpdateStatement;
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// FROM table_name
        /// JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public UpdateStatement Join<T>(JoinType type, Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, type, setFilter) as UpdateStatement;
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
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
