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


namespace Newq.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newq.Tests.Models;

    [TestClass]
    public class DeleteStatementTests
    {
        [TestMethod]
        public void Delete1()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Delete<Customer>()
                .Where((filter, context) => {
                    var cust = context["Customer"];

                    filter.Add(cust["City"].Between("New York", "Landon"));
                    filter.Add(cust["Name"].Like("Google").Or(cust["Name"].Like("Apple", Pattern.BeginWith)));
                });

            var result = queryBuilder.ToString();
            var expected = "DELETE FROM [Customer] WHERE [Customer].[City] BETWEEN 'New York' AND 'Landon' AND ([Customer].[Name] LIKE '%Google%' OR [Customer].[Name] LIKE 'Apple%') ";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Delete2()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Delete<Customer>();

            var result = queryBuilder.ToString();
            var expected = "DELETE FROM [Customer] ";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Delete3()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Delete<Customer>()
                .Join<Provider>((filter, context) => { })
                .Join<Country>((filter, context) => { })
                .Where((filter, context) => { });

            var result = queryBuilder.ToString();
            var expected = "DELETE FROM [Customer] ";

            Assert.AreEqual(expected, result);
        }
    }
}
