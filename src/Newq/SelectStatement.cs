namespace Newq
{
    using System;

    /// <summary>
    /// The SELECT statement is used to select data from a database.
    /// </summary>
    public class SelectStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectStatement"/> class.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="relatedTables"></param>
        public SelectStatement(DbTable table) : base(table)
        {

        }

        /// <summary>
        /// The DISTINCT keyword can be used to
        /// return only distinct (different) values.
        /// </summary>
        public bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TopNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Paginator Paginator { get; set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("SELECT {0}{1} FROM {2} ", GetParameters(), GetTarget(), DbContext[0]);
        }

        /// <summary>
        /// /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = base.ToSql();

            if (Paginator != null)
            {
                var startIndex = Paginator.CurrentRowNumber;
                var endIndex = Paginator.CurrentPage * Paginator.PageSize;
                var subQuery = string.Empty;
                var index = sql.IndexOf(" ORDER BY ");
                var rowNumberClause = string.Empty;

                if (index > -1)
                {
                    var orderByClause = sql.Substring(index).Trim();

                    rowNumberClause = string.Format(", ROW_NUMBER() OVER({0}) AS [ROW_NUMBER]", orderByClause);
                    subQuery = sql.Substring(0, index + 1).Insert(sql.IndexOf(" FROM "), rowNumberClause);
                }
                else
                {
                    var primaryKey = DbContext[0].PrimaryKey ?? DbContext[0].Columns[0];

                    rowNumberClause = string.Format(", ROW_NUMBER() OVER(ORDER BY {0}) AS [ROW_NUMBER]", primaryKey);
                    subQuery = sql.Insert(sql.IndexOf(" FROM "), rowNumberClause);
                }

                sql = string.Format(
                    "SELECT {0}{1} FROM ({2}) AS [PAGINATOR] WHERE [PAGINATOR].[ROW_NUMBER] BETWEEN {3} AND {4} ",
                    GetParameters(), GetTargetAlias(), subQuery.Trim(), startIndex, endIndex);
            }

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetTarget()
        {
            var target = string.Empty;
            var items = Target.GetTargetObjects();

            if (items.Count == 0)
            {
                foreach (var tab in DbContext.Tables)
                {
                    foreach (var col in tab.Columns)
                    {
                        target += string.Format("{0} AS {1}, ", col, col.Alias);
                    }
                }
            }
            else
            {
                DbColumn column;
                Function function;

                foreach (var item in items)
                {
                    if (item is DbColumn)
                    {
                        column = item as DbColumn;
                        target += string.Format("{0} AS {1}, ", column, column.Alias);
                    }
                    else if (item is Function)
                    {
                        function = item as Function;
                        target += string.Format("{0} AS {1}, ", function, function.Alias);
                    }
                }
            }

            return target.Remove(target.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetTargetAlias()
        {
            var target = string.Empty;
            var alias = string.Empty;
            var items = Target.GetTargetObjects();

            if (items.Count == 0)
            {
                foreach (var tab in DbContext.Tables)
                {
                    foreach (var col in tab.Columns)
                    {
                        alias = col.Alias;
                        target += string.Format("{0}, ", alias);
                    }
                }
            }
            else
            {
                foreach (var item in items)
                {
                    if (item is DbColumn)
                    {
                        alias = (item as DbColumn).Alias;
                    }
                    else if (item is Function)
                    {
                        alias = (item as Function).Alias;
                    }

                    target += string.Format("{0}, ", alias);
                }
            }

            return target.Remove(target.Length - 2);
        }

        private string GetParameters()
        {
            var distinct = IsDistinct ? "DISTINCT " : string.Empty;
            var topClause = string.Empty;

            if (TopNumber > 0)
            {
                topClause = IsPercent
                    ? string.Format("TOP({0}) PERCENT ", TopNumber)
                    : string.Format("TOP({0}) ", TopNumber);
            }

            return distinct + topClause;
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name1
        /// INNER JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public SelectStatement Join<T>(Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, setFilter) as SelectStatement;
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name1
        /// JOIN table_name2
        /// ON table_name1.column_name=table_name2.column_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public SelectStatement Join<T>(JoinType type, Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, type, setFilter) as SelectStatement;
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name
        /// WHERE column_name operator value
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public SelectWhereClause Where(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new SelectWhereClause(this), setFilter) as SelectWhereClause;
        }

        /// <summary>
        /// SELECT column_name, aggregate_function(column_name)
        /// FROM table_name
        /// WHERE column_name operator value
        /// GROUP BY column_name
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public GroupByClause GroupBy(Action<Target> setTarget)
        {
            return Provider.SetTarget(new GroupByClause(this), setTarget) as GroupByClause;
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name
        /// ORDER BY column_name[ASC | DESC]
        /// </summary>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public OrderByClause OrderBy(Action<Target> setTarget)
        {
            return Provider.SetTarget(new OrderByClause(this), setTarget) as OrderByClause;
        }
    }
}
