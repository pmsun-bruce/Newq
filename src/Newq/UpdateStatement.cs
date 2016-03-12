namespace Newq
{
    using Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The UPDATE statement is used to
    /// update existing records in a table.
    /// </summary>
    public class UpdateStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStatement"/> class.
        /// </summary>
        /// <param name="table"></param>
        public UpdateStatement(Table table) : base(table)
        {
            Target = new Target(Context);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomizable<Action<Target, Context>> Target { get; }

        /// <summary>
        /// 
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = string.Format("UPDATE {0} SET {1} ", Context[0], GetTarget());

            if (Clauses.Count > 0)
            {
                var isFirstJoinClause = true;

                foreach (var cls in Clauses)
                {
                    if (cls is JoinClause && isFirstJoinClause)
                    {
                        sql += string.Format("FROM {0} ", Context[0]);
                        isFirstJoinClause = false;
                    }

                    sql += cls.ToSql();
                }
            }

            return sql;
        }

        /// <summary>
        /// Returns a string that represents the current statement target.
        /// </summary>
        /// <returns></returns>
        protected string GetTarget()
        {
            var target = string.Empty;

            if (Object == null)
            {
                Target.Perform();

                var items = (Target as Target).Items;

                if (items.Count == 0)
                {
                    foreach (var col in Context[0].Columns)
                    {
                        target += string.Format("{0} = {1}, ", col, col.Value.ToSqlValue());
                    }
                }
                else
                {
                    Column col = null;

                    foreach (var item in items)
                    {
                        if (item is Column)
                        {
                            col = item as Column;
                            target += string.Format("{0} = {1}, ", col, col.Value.ToSqlValue());
                        }
                    }
                }
            }
            else
            {
                var type = Object.GetType();

                foreach (var col in Context[0].Columns)
                {
                    target += string.Format("{0} = {1}, ", col, type.GetProperty(col.Name).GetValue(Object).ToSqlValue());
                }
            }

            return target.Remove(target.Length - 2);
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// FROM table_name
        /// INNER JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customization"></param>
        /// <returns></returns>
        public UpdateStatement Join<T>(Action<Filter, Context> customization)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, customization) as UpdateStatement;
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
        /// <param name="customization"></param>
        /// <returns></returns>
        public UpdateStatement Join<T>(JoinType type, Action<Filter, Context> customization)
        {
            return Provider.Join<T>(this, type, customization) as UpdateStatement;
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// WHERE column_name operator value
        /// </summary>
        /// <param name="customization"></param>
        /// <returns></returns>
        public WhereClause Where(Action<Filter, Context> customization)
        {
            return Provider.Filtrate(new WhereClause(this), customization);
        }
    }
}
