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
        /// Initializes a new instance of the <see cref="Statement"/> class.
        /// </summary>
        protected Statement()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Statement"/> class.
        /// </summary>
        /// <param name="table"></param>
        protected Statement(DbTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            DbContext = new DbContext(table);
            Clauses = new List<Clause>();
            Target = new Target(DbContext);
        }

        /// <summary>
        /// Database context of statement.
        /// </summary>
        public DbContext DbContext { get; protected set; }

        /// <summary>
        /// Clauses of statement.
        /// </summary>
        public List<Clause> Clauses { get; protected set; }

        /// <summary>
        /// Target of statement.
        /// </summary>
        public Target Target { get; protected set; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
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
        /// Gets the target of statement.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTarget()
        {
            return Target.ToString();
        }

        /// <summary>
        /// Provide sql methods.
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
            public static Clause SetTarget(Clause clause, Action<Target> setTarget)
            {
                clause.Statement.Clauses.Add(clause);
                setTarget(clause.Target);

                return clause;
            }
        }
    }
}
