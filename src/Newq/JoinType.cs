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
    /// <summary>
    /// 
    /// </summary>
    public enum JoinType
    {
        /// <summary>
        /// The INNER JOIN selects all rows from both tables
        /// as long as there is a match between the columns in both tables.
        /// </summary>
        InnerJoin,

        /// <summary>
        /// The LEFT JOIN returns all rows from the left table (table1),
        /// with the matching rows in the right table (table2).
        /// The result is NULL in the right side when there is no match.
        /// </summary>
        LeftJoin,

        /// <summary>
        /// The RIGHT JOIN returns all rows from the right table (table2),
        /// with the matching rows in the left table (table1).
        /// The result is NULL in the left side when there is no match.
        /// </summary>
        RightJoin,

        /// <summary>
        /// The FULL JOIN returns all rows from the left table (table1)
        /// and from the right table (table2).
        /// </summary>
        FullJoin,

        /// <summary>
        /// The Cross JOIN returns all rows where each row from the left table (table1)
        /// is combined with each row from the right table (table2).
        /// </summary>
        CrossJoin,
    }
}
