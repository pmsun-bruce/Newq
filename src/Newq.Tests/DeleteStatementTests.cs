using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Models;

namespace Newq.Tests
{
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
