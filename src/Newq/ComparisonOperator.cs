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
    public enum ComparisonOperator
    {
        /// <summary>
        /// A comparison operator for greater than a specified value.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// A comparison operator for greater than or equal to a specified value.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// A comparison operator for less than a specified value.
        /// </summary>
        LessThan,

        /// <summary>
        /// A comparison operator for less than or equal to a specified value.
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// A comparison operator for equal to a specified value.
        /// </summary>
        EqualTo,

        /// <summary>
        /// A comparison operator for not equal to a specified value.
        /// </summary>
        NotEqualTo,

        /// <summary>
        /// The LIKE operator is used to
        /// search for a specified pattern in a column.
        /// </summary>
        Like,

        /// <summary>
        /// The NOT LIKE operator is used to
        /// search for not match a specified pattern in a column.
        /// </summary>
        NotLike,

        /// <summary>
        /// The IN operator allows you to
        /// specify multiple values in a WHERE clause.
        /// </summary>
        In,

        /// <summary>
        /// The NOT IN operator allows you to
        /// specify multiple exceptive values in a WHERE clause.
        /// </summary>
        NotIn,

        /// <summary>
        /// The BETWEEN operator selects values within a range.
        /// The values can be numbers, text, or dates.
        /// </summary>
        Between,

        /// <summary>
        /// The NOT BETWEEN operator selects values not within a range.
        /// The values can be numbers, text, or dates.
        /// </summary>
        NotBetween,

        /// <summary>
        /// 
        /// </summary>
        IsNull,

        /// <summary>
        /// 
        /// </summary>
        IsNotNull,
    }
}
