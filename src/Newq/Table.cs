/* Copyright 2015-2016 Andrew Lyu and Uriel Van
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
    /// Simulate tables in Sql
    /// </summary>
    public class Table
    {
        /// <summary>
        /// An dictionary used to get columns in the table.
        /// </summary>
        protected Dictionary<string, Column> columns;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="type">Type of the corresponding class</param>
        public Table(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            columns = new Dictionary<string, Column>();
            Name = type.Name;

            Column column = null;
            var defaultPK = "Id";
            var properties = type.GetProperties()
                .Where(p => p.CanRead &&
                            p.CanWrite &&
                            p.PropertyType.Namespace == "System" &&
                            !p.CustomAttributes.Any(a => a.AttributeType == typeof(NonColumnAttribute)));

            if (properties.Any(p => p.Name == defaultPK))
            {
                column = new Column(this, defaultPK);
                columns.Add(defaultPK, column);
                properties = properties.Where(p => p.Name != defaultPK);
            }

            foreach (var prop in properties)
            {
                column = new Column(this, prop.Name);
                columns.Add(prop.Name, column);
            }
        }

        /// <summary>
        /// Gets or sets <see cref="Name"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// A read-only list contains the columns in this.
        /// </summary>
        public IReadOnlyList<Column> Columns
        {
            get { return columns.Values.ToList(); }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by column name.
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <returns>an entity of column</returns>
        public Column this[string columnName]
        {
            get { return columns[columnName]; }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by column name.
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="exclude"></param>
        /// <returns>an entity of column</returns>
        public Column this[string columnName, Exclude exclude]
        {
            get
            {
                var column = columns[columnName];

                column.ExcludePattern = exclude;

                return column;
            }
        }

        /// <summary>
        /// Gets <see cref="OrderByColumn"/> by column name and sort order.
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="order"></param>
        /// <returns>an entity of column</returns>
        public OrderByColumn this[string columnName, SortOrder order]
        {
            get { return new OrderByColumn(columns[columnName], order); }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>Name of this with a pair of square brackets</returns>
        public override string ToString()
        {
            return string.Format("[{0}]", Name);
        }
    }
    
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class NonColumnAttribute : Attribute
    {
    }
}
