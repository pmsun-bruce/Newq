namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to build SQL statement.
    /// </summary>
    public class QueryBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        public QueryBuilder()
        {

        }

        /// <summary>
        /// Gets or sets <see cref="Statement"/>.
        /// </summary>
        public Statement Statement { get; protected set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Statement?.ToSql() ?? string.Empty;
        }

        /// <summary>
        /// Define whether the statement is paginable.
        /// </summary>
        /// <param name="paginator"></param>
        /// <returns>true when the statement is select statement, false if not</returns>
        public bool Paginate(Paginator paginator)
        {
            var isPaginable = false;

            if (Statement is SelectStatement)
            {
                (Statement as SelectStatement).Paginator = paginator;
                isPaginable = true;
            }

            return isPaginable;
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <returns>
        /// SelectStatement to select all records
        /// from the certain table
        /// without the keyword distinct
        /// </returns>
        public SelectStatement Select<T>()
        {
            return Select<T>(false, 0, false, null);
        }

        /// <summary>
        /// SELECT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="topRows">Number of the top records to select</param>
        /// <param name="isPercent">
        /// Determine whether the topRows represent the percentage,
        /// default is false
        /// </param>
        /// <returns>
        /// SelectStatement to select certain quantity(percentage if isPercent is true)
        /// of the top records from the table
        /// </returns>
        public SelectStatement Select<T>(int topRows, bool isPercent = false)
        {
            return Select<T>(false, topRows, false, null);
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="customization">Determine which column(s) to be selected from the table</param>
        /// <returns>SelectStatement to select all records within certain columns from the table without the keyword distinct</returns>
        public SelectStatement Select<T>(Action<Target, Context> customization)
        {
            return Select<T>(false, 0, false, customization);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="topRows"></param>
        /// <param name="customization"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(int topRows, Action<Target, Context> customization)
        {
            return Select<T>(false, topRows, false, customization);
        }

        /// <summary>
        /// SELECT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topRows"></param>
        /// <param name="isPercent"></param>
        /// <param name="customization"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(int topRows, bool isPercent, Action<Target, Context> customization)
        {
            return Select<T>(false, topRows, isPercent, customization);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isDistinct"></param>
        /// <param name="topRows"></param>
        /// <param name="isPercent"></param>
        /// <param name="customization"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(bool isDistinct, int topRows, bool isPercent, Action<Target, Context> customization)
        {
            var statement = new SelectStatement(new Table(typeof(T)));

            Statement = statement;
            statement.IsDistinct = isDistinct;
            statement.TopRows = topRows;
            statement.IsPercent = isPercent;
            statement.Target.Customize(customization);

            return statement;
        }

        /// <summary>
        /// SELECT DISTINCT column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>()
        {
            return Select<T>(true, 0, false, null);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topRows"></param>
        /// <param name="isPercent"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topRows, bool isPercent = false)
        {
            return Select<T>(true, topRows, isPercent, null);
        }

        /// <summary>
        /// SELECT DISTINCT column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="customization">Determine which column(s) to be selected from the table</param>
        /// <returns>SelectStatement to select all records within certain columns from the table without the keyword distinct</returns>
        public SelectStatement SelectDistinct<T>(Action<Target, Context> customization)
        {
            return Select<T>(true, 0, false, customization);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topRows"></param>
        /// <param name="customization"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topRows, Action<Target, Context> customization)
        {
            return Select<T>(true, topRows, false, customization);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topRows"></param>
        /// <param name="isPercent"></param>
        /// <param name="customization"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topRows, bool isPercent, Action<Target, Context> customization)
        {
            return Select<T>(true, topRows, isPercent, customization);
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public UpdateStatement Update<T>(T obj)
        {
            var statement = new UpdateStatement(new Table(typeof(T)));

            Statement = statement;
            statement.Object = obj;

            return statement;
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customization"></param>
        /// <returns></returns>
        public UpdateStatement Update<T>(Action<Target, Context> customization)
        {
            var statement = new UpdateStatement(new Table(typeof(T)));

            Statement = statement;
            statement.Target.Customize(customization);

            return statement;
        }

        /// <summary>
        /// INSERT INTO table_name
        /// (column1, column2, column3,...)
        /// VALUES(value1, value2, value3,...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public InsertStatement Insert<T>(T obj)
        {
            var statement = new InsertStatement(new Table(typeof(T)));

            Statement = statement;
            statement.ObjectList.Add(obj);

            return statement;
        }

        /// <summary>
        /// INSERT INTO table_name
        /// (column1, column2, column3,...)
        /// VALUES(value1, value2, value3,...),(value1, value2, value3,...),...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <returns></returns>
        public InsertStatement Insert<T>(List<T> objList)
        {
            var statement = new InsertStatement(new Table(typeof(T)));

            Statement = statement;
            objList.ForEach(obj => statement.ObjectList.Add(obj));

            return statement;
        }

        /// <summary>
        /// DELETE FROM table_name 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DeleteStatement Delete<T>()
        {
            return Delete<T>(0, false);
        }

        /// <summary>
        /// DELETE TOP number|percent FROM table_name 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topRows"></param>
        /// <param name="isPercent"></param>
        /// <returns></returns>
        public DeleteStatement Delete<T>(int topRows, bool isPercent = false)
        {
            var statement = new DeleteStatement(new Table(typeof(T)));

            Statement = statement;
            statement.TopRows = topRows;
            statement.IsPercent = isPercent;

            return statement;
        }
    }
}
