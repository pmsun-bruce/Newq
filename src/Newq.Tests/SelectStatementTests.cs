using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Entitites;

namespace Newq.Tests
{
    [TestClass]
    public class SelectStatementTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Select<Customer>(target => {
                    target.Add(target.DbContext["Provider", "Products"]);
                })

                .Join<Provider>(JoinType.LeftJoin, filter => {
                    filter.Add(filter.DbContext["Customer", "Name"].EqualTo(filter.DbContext["Provider", "Name"]));
                })

                .Where(filter => {
                    filter.Add(filter.DbContext["Customer", "City"].Like("New"));
                })

                .GroupBy(target => {
                    target.Add(target.DbContext["Provider", "Products"]);
                })

                .Having(filter => {
                    filter.Add(filter.DbContext["Provider", "Name"].NotLike("New"));
                })

                .OrderBy(target => {
                    target.Add(target.DbContext["Customer", "Name"], SortOrder.Desc);
                });

            queryBuilder.Paginate(new Paginator());

            var query = queryBuilder.ToString();

            /*  Result:
                SELECT 
                    [Provider.Products] 
                FROM (
                    SELECT 
                        [Provider].[Products] AS [Provider.Products]
                        , ROW_NUMBER() OVER(ORDER BY [Customer].[Name] DESC) AS [ROW_NUMBER] 
                    FROM 
                        [Customer] 
                    LEFT JOIN 
                        [Provider] 
                    ON 
                        [Customer].[Name] = [Provider].[Name] 
                    WHERE 
                        [Customer].[City] LIKE '%New%' 
                    GROUP BY 
                        [Provider].[Products] 
                    Having 
                        [Provider].[Name] NOT LIKE '%New%' 
                ) AS [PAGINATOR] 
                WHERE 
                    [PAGINATOR].[ROW_NUMBER] BETWEEN 1 AND 10 
            */
        }
    }
}
