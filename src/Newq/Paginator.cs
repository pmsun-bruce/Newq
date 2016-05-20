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
    public class Paginator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paginator"/> class.
        /// </summary>
        public Paginator()
        {
            TotalRows = 0;
            PageSize = 10;
            CurrentPage = 1;
        }

        /// <summary>
        /// Gets or sets <see cref="TotalRows"/>.
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets <see cref="TotalPages"/>.
        /// </summary>
        public int TotalPages
        {
            get
            {
                var pageSize = PageSize > 0
                             ? PageSize
                             : TotalRows > 0 ? TotalRows : 1;
                             
                var total = TotalRows / pageSize;

                return TotalRows % pageSize == 0 ? total : total + 1;
            }
        }

        /// <summary>
        /// Gets or sets <see cref="PageSize"/>.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CurrentPage"/>.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets <see cref="BeginRowNumber"/>.
        /// </summary>
        public int BeginRowNumber
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        /// <summary>
        /// Gets <see cref="EndRowNumber"/>.
        /// </summary>
        public int EndRowNumber
        {
            get { return CurrentPage * PageSize; }
        }
    }
}
