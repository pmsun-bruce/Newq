namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// SQL Statement
    /// </summary>
    public abstract class Statement
    {
        /// <summary>
        /// 
        /// </summary>
        protected Statement()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="relatedTables"></param>
        protected Statement(DbTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            DbContext = new DbContext(table);
            Clauses = new List<Clause>();
            Target = new TargetColumns(DbContext);
        }

        /// <summary>
        /// Database context
        /// </summary>
        public DbContext DbContext { get; protected set; }

        /// <summary>
        /// Clauses of this Statement
        /// </summary>
        public List<Clause> Clauses { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public TargetColumns Target { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ToSql()
        {
            var sql = this.ToString();
            
            if (Clauses.Count > 0)
            {
                Clauses.ForEach(clause => sql += clause.ToSql());
            }
            
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetTarget()
        {
            return Target.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected static class Provider
        {
            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="type"></param>
            /// <param name="setFilter"></param>
            /// <returns></returns>
            public static Statement Join<T>(Statement statement, JoinType type, Action<Filter> setFilter)
            {
                var tableName = typeof(T).Name;
                JoinClause clause = null;

                if (statement.DbContext.Contains(tableName))
                {
                    clause = new JoinClause(statement, statement.DbContext[tableName], type);
                }
                else
                {
                    var table = new DbTable(typeof(T));
                    statement.DbContext.Add(table);
                    clause = new JoinClause(statement, table, type);
                }

                statement.Clauses.Add(clause);
                setFilter(clause.Filter);

                return statement;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="clause"></param>
            /// <param name="setFilter"></param>
            /// <returns></returns>
            public static Clause Filtrate(Clause clause, Action<Filter> setFilter)
            {
                clause.Statement.Clauses.Add(clause);
                setFilter(clause.Filter);

                return clause;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="clause"></param>
            /// <param name="setTarget"></param>
            /// <returns></returns>
            public static Clause SetTarget(Clause clause, Action<TargetColumns> setTarget)
            {
                clause.Statement.Clauses.Add(clause);
                setTarget(clause.Target);

                return clause;
            }
        }
    }
}
