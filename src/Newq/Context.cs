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
        /// 
        /// </summary>
        public IReadOnlyList<Table> Tables
        {
            get { return tables.Values.ToList(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Table this[int index]
        {
            get { return Tables[index]; }
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool Contains(string tableName)
        {
            return tables.ContainsKey(tableName);
        }

        /// <summary>
        /// Add a table to current database context.
        /// </summary>
        /// <param name="table"></param>
        public void Add(Table table)
        {
            tables.Add(table.Name, table);
        }
    }
}
