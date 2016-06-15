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
    using FakeItEasy;
    using Newq.Tests.Models;
    using Xunit;

    public class UpdateStatementTests
    {
        [Fact]
        public void Update1()
        {
            var customer = new Customer();
            customer.City = "New York";
            customer.Remark = "Good Customer";

            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Update(customer);

            /*  Result:
                UPDATE [Customer]
                SET
                    [Customer].[Id] = ''
                    , [Customer].[Name] = ''
                    , [Customer].[City] = 'New York'
                    , [Customer].[Remark] = 'Good Customer'
                    , [Customer].[Status] = 0
                    , [Customer].[Flag] = 0
                    , [Customer].[Version] = 0
                    , [Customer].[AuthorId] = ''
                    , [Customer].[EditorId] = ''
                    , [Customer].[CreatedDate] = '0001-01-01 12:00:00 000'
                    , [Customer].[ModifiedDate] = '0001-01-01 12:00:00 000'
            */
        }

        [Fact]
        public void Update2()
        {
            var customer = new Customer();
            customer.City = "New York";
            customer.Remark = "Good Customer";

            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Update(customer)
                .Join<Provider>((filter, context) => filter.Add(context[0]["Name"].EqualTo(context[1]["Name"])));

            var query = queryBuilder.ToString();

            /*  Result:
                UPDATE
                    [Customer]
                SET
                    [Customer].[Id] = ''
                    , [Customer].[Name] = ''
                    , [Customer].[City] = 'New York'
                    , [Customer].[Remark] = 'Good Customer'
                    , [Customer].[Status] = 0
                    , [Customer].[Flag] = 0
                    , [Customer].[Version] = 0
                    , [Customer].[AuthorId] = ''
                    , [Customer].[EditorId] = ''
                    , [Customer].[CreatedDate] = '0001-01-01 12:00:00 000'
                    , [Customer].[ModifiedDate] = '0001-01-01 12:00:00 000'
                FROM
                    [Customer]
                INNER JOIN
                    [Provider]
                ON
                    [Customer].[Name] = [Provider].[Name]
            */
        }
    }
}
