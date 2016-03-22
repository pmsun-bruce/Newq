using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Models;

namespace Newq.Tests
{
    [TestClass]
    public class DeleteStatement
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

            /*  Result:
                DELETE [Customer] 
                FROM [Customer] 
                WHERE 
                    [Customer].[City] Between 'New York' AND 'Landon' 
                    AND 
                    (
                        [Customer].[Name] LIKE '%Google%' 
                        OR 
                        [Customer].[Name] LIKE 'Apple%'
                    ) 
            */
        }
    }
}
