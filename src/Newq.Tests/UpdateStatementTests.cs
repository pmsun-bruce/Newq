using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Entitites;

namespace Newq.Tests
{
    [TestClass]
    public class UpdateStatementTests
    {
        [TestMethod]
        public void TestMethod1()
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

        [TestMethod]
        public void TestMethod2()
        {
            var customer = new Customer();
            customer.City = "New York";
            customer.Remark = "Good Customer";

            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Update(customer)
                .Join<Provider>(filter => filter.Add(filter.DbContext[0]["Name"].EqualTo(filter.DbContext[1]["Name"])));

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
