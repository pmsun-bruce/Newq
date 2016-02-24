namespace Newq
{
    using System;

    /// <summary>
    /// 
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

            return string.Format("UPDATE {0} SET {1} FROM {0}", DbContext[0], setClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public UpdateStatement Join<T>(Action<Filter> setFilter)
        {
            return Provider.Join<T>(this, JoinType.InnerJoin, setFilter) as UpdateStatement;
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="setFilter"></param>
        /// <returns></returns>
        public WhereClause Where(Action<Filter> setFilter)
        {
            return Provider.Filtrate(new WhereClause(this), setFilter) as WhereClause;
        }
    }
}
