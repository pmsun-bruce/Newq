using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Models;

namespace Newq.Tests
{
    [TestClass]
    public class InsertStatement
    {
        [TestMethod]
        public void Insert1()
        {
            var customer = new Customer();
            customer.City = "New York";
            customer.Remark = "Good Customer";

            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Insert(customer);

            /*  Result:
                INSERT INTO [Customer] 
                (
                    [Customer].[Id]
                    , [Customer].[Name]
                    , [Customer].[City]
                    , [Customer].[Remark]
                    , [Customer].[Status]
                    , [Customer].[Flag]
                    , [Customer].[Version]
                    , [Customer].[AuthorId]
                    , [Customer].[EditorId]
                    , [Customer].[CreatedDate]
                    , [Customer].[ModifiedDate]
                ) VALUES (
                    ''
                    , ''
                    , 'New York'
                    , 'Good Customer'
                    , 0
                    , 0
                    , 0
                    , ''
                    , ''
                    , '0001-01-01 12:00:00 000'
                    , '0001-01-01 12:00:00 000'
                ) 
            */
        }
    }
}
