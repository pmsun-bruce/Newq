namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Database context for statement or clause.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, Table> tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="table"></param>
        public Context(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            tables = new Dictionary<string, Table>();
            tables.Add(table.Name, table);
        }

        /// <summary>
        /// Gets <see cref="Tables"/>.
        /// </summary>
        public IReadOnlyList<Table> Tables
        {
            get { return tables.Values.ToList(); }
        }

        /// <summary>
        /// Gets <see cref="Table"/> by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Table this[int index]
        {
            get { return Tables[index]; }
        }

        /// <summary>
        /// Gets <see cref="Table"/> by table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public Table this[string tableName]
        {
            get
            {
                if (!Contains(tableName))
                {
                    return new Table(tableName);
                }

                return tables[tableName];
            }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by table name and column name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Column this[string tableName, string columnName]
        {
            get
            {
                if (!Contains(tableName))
                {
                    return new Column(tableName, columnName);
                }

                return tables[tableName][columnName];
            }
        }

        /// <summary>
        /// Gets <see cref="KeyValuePair{TKey, TValue}"/> by table name, column name and sort order.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderByColumn this[string tableName, string columnName, SortOrder order]
        {
            get
            {
                if (!Contains(tableName))
                {
                    var column = new Column(tableName, columnName);

                    return new OrderByColumn(column, order);
                }

                return tables[tableName][columnName, order];
            }
        }

        /// <summary>
        /// Determines whether a <see cref="Table"/> is in the context.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool Contains(string tableName)
        {
            return tables.ContainsKey(tableName);
        }

        /// <summary>
        /// Adds a <see cref="Table"/> to the end of the context.
        /// </summary>
        /// <param name="table"></param>
        public void Add(Table table)
        {
            tables.Add(table.Name, table);
        }
    }
}
