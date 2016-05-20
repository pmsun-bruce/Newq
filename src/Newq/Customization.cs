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
    using System.Linq.Expressions;

    /// <summary>
    /// 
    /// </summary>
    public abstract class Customization
    {
        /// <summary>
        /// 
        /// </summary>
        protected Context context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Customization(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }

        /// <summary>
        /// Returns a string that represents the current customization.
        /// </summary>
        /// <returns></returns>
        public abstract string GetCustomization();
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public Column Table<T>(Expression<Func<T, object>> expr)
        {
            var split = expr.Body.ToString().Split("(.)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var columnName = split[split.Length - 1];
            
            return context[typeof(T).Name, columnName];
        }
    }
}
