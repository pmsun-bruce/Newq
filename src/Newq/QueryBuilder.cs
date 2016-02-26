namespace Newq
{
    using System;

    /// <summary>
    /// Used to build SQL statement
    /// </summary>
    public class QueryBuilder
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QueryBuilder()
        {

        }

        /// <summary>
        /// Record different statement
        /// </summary>
        public Statement Statement { get; private set; }

        /// <summary>
        /// Override ToString function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Statement?.ToSql() ?? string.Empty;
        }

        /// <summary>
        /// Define whether the statement is paginable
        /// </summary>
        /// <param name="paginator">instance of Paginator</param>
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
            return Select<T>(false, 0, false, col => { });
        }

        /// <summary>
        /// SELECT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="topNumber">Number of the top records to select</param>
        /// <param name="isPercent">
        /// Determine whether the topNumber represent the percentage,
        /// default is false
        /// </param>
        /// <returns>
        /// SelectStatement to select certain quantity(percentage if isPercent is true)
        /// of the top records from the table
        /// </returns>
        public SelectStatement Select<T>(int topNumber, bool isPercent = false)
        {
            return Select<T>(false, topNumber, false, target => { });
        }

        /// <summary>
        /// SELECT column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="setTarget">Determine which column(s) to be selected from the table</param>
        /// <returns>SelectStatement to select all records within certain columns from the table without the keyword distinct</returns>
        public SelectStatement Select<T>(Action<TargetColumns> setTarget)
        {
            return Select<T>(false, 0, false, setTarget);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="topNumber"></param>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(int topNumber, Action<TargetColumns> setTarget)
        {
            return Select<T>(false, topNumber, false, setTarget);
        }

        /// <summary>
        /// SELECT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topNumber"></param>
        /// <param name="isPercent"></param>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(int topNumber, bool isPercent, Action<TargetColumns> setTarget)
        {
            return Select<T>(false, topNumber, isPercent, setTarget);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isDistinct"></param>
        /// <param name="topNumber"></param>
        /// <param name="isPercent"></param>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public SelectStatement Select<T>(bool isDistinct, int topNumber, bool isPercent, Action<TargetColumns> setTarget)
        {
            var statement = new SelectStatement(new DbTable(typeof(T)));

            Statement = statement;
            statement.IsDistinct = isDistinct;
            statement.TopNumber = topNumber;
            statement.IsPercent = isPercent;
            setTarget(statement.Target);

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
            return Select<T>(true, 0, false, col => { });
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topNumber"></param>
        /// <param name="isPercent"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topNumber, bool isPercent = false)
        {
            return Select<T>(false, topNumber, isPercent, col => { });
        }

        /// <summary>
        /// SELECT DISTINCT TOP number column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topNumber"></param>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topNumber, Action<TargetColumns> setTarget)
        {
            return Select<T>(true, topNumber, false, setTarget);
        }

        /// <summary>
        /// SELECT DISTINCT TOP number|percent column_name(s)
        /// FROM table_name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topNumber"></param>
        /// <param name="isPercent"></param>
        /// <param name="setTarget"></param>
        /// <returns></returns>
        public SelectStatement SelectDistinct<T>(int topNumber, bool isPercent, Action<TargetColumns> setTarget)
        {
            return Select<T>(true, topNumber, isPercent, setTarget);
        }

        /// <summary>
        /// UPDATE table_name
        /// SET column1 = value, column2 = value,...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UpdateStatement Update<T>(T entity)
        {
            var statement = new UpdateStatement(new DbTable(entity));

            Statement = statement;

            return statement;
        }

        /// <summary>
        /// INSERT INTO table_name
        /// (column1, column2, column3,...)
        /// VALUES(value1, value2, value3,....)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public InsertStatement Insert<T>(T entity)
        {
            var statement = new InsertStatement(new DbTable(entity));

            Statement = statement;

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
        /// <param name="topNumber"></param>
        /// <param name="isPercent"></param>
        /// <returns></returns>
        public DeleteStatement Delete<T>(int topNumber, bool isPercent = false)
        {
            var statement = new DeleteStatement(new DbTable(typeof(T)));

            Statement = statement;
            statement.TopNumber = topNumber;
            statement.IsPercent = isPercent;

            return statement;
        }
    }
}
