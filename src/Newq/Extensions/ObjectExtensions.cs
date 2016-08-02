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


namespace Newq.Extensions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSqlValue(this object obj)
        {
            var value = string.Empty;

            if (obj is string)
            {
                value = string.Format("'{0}'", obj.ToString().Replace("'", "''"));
            }
            else if (obj is DateTime)
            {
                var time = (DateTime)obj;

                value = time.Year < 1753 ? "'1753-01-01 00:00:00.000'"
                    : string.Format("'{0}'", time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            else if (obj == null)
            {
                value = "''";
            }
            else
            {
                value = obj.ToString();
            }

            return value;
        }
    }
}
