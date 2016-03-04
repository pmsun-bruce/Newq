using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Models;

namespace Newq.Tests
{
    [TestClass]
    public class DeleteStatement
    {
        [TestMethod]
        public void TestMethod1()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Delete<Customer>()
                .Where(con => {
                    var cust = con.Context["Customer"];

                    con.Add(cust["City"].Between("New York", "Landon"));
                    con.Add(cust["Name"].Like("Google").Or(cust["Name"].Like("Apple", PatternType.BeginWith)));
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
