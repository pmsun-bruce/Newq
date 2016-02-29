namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Database context for statement or clause.
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, DbTable> tables;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables"></param>
        public DbContext(DbTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            tables = new Dictionary<string, DbTable>();
            tables.Add(table.Name, table);
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<DbTable> Tables
        {
            get { return tables.Values.ToList(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DbTable this[int index]
        {
            get { return Tables[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DbTable this[string tableName]
        {
            get
            {
                if (!Contains(tableName))
                {
                    return new DbTable(tableName);
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
        public DbColumn this[string tableName, string columnName]
        {
            get
            {
                if (!Contains(tableName))
                {
                    return new DbColumn(tableName, columnName);
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
        public void Add(DbTable table)
        {
            tables.Add(table.Name, table);
        }
    }
}
