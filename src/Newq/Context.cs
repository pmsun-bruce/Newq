/* Copyright 2015-2016 Andrew Lyu & Uriel Van
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


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
        protected Dictionary<string, Table> tables;

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
            get { return tables[tableName]; }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by table name and column name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Column this[string tableName, string columnName]
        {
            get { return tables[tableName][columnName]; }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by table name and column name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public Column this[string tableName, string columnName, Exclude exclude]
        {
            get { return tables[tableName][columnName, exclude]; }
        }

        /// <summary>
        /// Gets <see cref="OrderByColumn"/> by table name, column name and sort order.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderByColumn this[string tableName, string columnName, SortOrder order]
        {
            get { return tables[tableName][columnName, order]; }
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
