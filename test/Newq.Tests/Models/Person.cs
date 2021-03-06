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

namespace Newq.Tests.Models
{
    using Attributes;

    [Table("PERSION")]
    public class Person
    {
        protected string email;

        public string Name { get; set; }

        [ColumnIgnore]
        public int Age { get; set; }

        public Country Country { get; set; }

        [PrimaryKey]
        [Column("ID")]
        public string id { get; set; }

    }
}
