﻿/* Copyright 2015-2016 Andrew Lyu and Uriel Van
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
    using Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The INSERT INTO statement is used to
    /// insert new records in a table.
    /// </summary>
    public class InsertStatement : Statement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertStatement"/> class.
        /// </summary>
        /// <param name="table"></param>
        public InsertStatement(Table table)
            : base(table)
        {
            ObjectList = new List<object>();
        }

        /// <summary>
        /// Gets <see cref="ObjectList"/>.
        /// </summary>
        public List<object> ObjectList { get; }

        /// <summary>
        /// Returns a SQL-string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToSql()
        {
            var columns = string.Empty;
            var values = string.Empty;
            var valueClause = string.Empty;
            Type type = null;

            foreach (var col in Context[0].Columns)
            {
                columns += string.Format(",{0}", col);
            }

            ObjectList.ForEach(obj => {
                type = obj.GetType();

                foreach (var col in Context[0].Columns)
                {
                    values += string.Format(",{0}", type.GetProperty(col.Name).GetValue(obj).ToSqlValue());
                }

                valueClause += string.Format(",({0})", values.Substring(1));
                values = string.Empty;
            });

            valueClause = string.Format("({0}) VALUES {1}", columns.Substring(1), valueClause.Substring(1));

            return string.Format("INSERT INTO {0} {1} ", Context[0], valueClause);
        }
    }
}
