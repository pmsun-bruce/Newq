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

    /// <summary>
    /// The WHERE clause is used to
    /// extract only those records that fulfill a specified criterion.
    /// </summary>
    public class WhereClause : Clause
    {
        /// <summary>
        /// 
        /// </summary>
        protected Filter filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhereClause"/> class.
        /// </summary>
        /// <param name="statement"></param>
        public WhereClause(Statement statement)
            : base(statement)
        {
            filter = new Filter(statement.Context);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomizable<Action<Filter, Context>> Filter
        {
            get { return filter; }
        }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var sql = filter.GetCustomization();

            if (sql.Length > 0)
            {
                sql = string.Format("WHERE {0} ", sql);
            }

            return sql;
        }
    }
}
