using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newq.Tests.Models;
using System.Collections.Generic;

namespace Newq.Tests
{
    [TestClass]
    public class SelectStatementTests
    {
        [TestMethod]
        public void Select1()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Select<Customer>(target => {
                    target.Add(target.Context["Provider", "Products"]);
                })

                .Join<Provider>(JoinType.LeftJoin, filter => {
                    filter.Add(filter.Context["Customer", "Name"].EqualTo(filter.Context["Provider", "Name"]));
                })

                .Where(filter => {
                    filter.Add(filter.Context["Customer", "City"].Like("New"));
                })

                .GroupBy(target => {
                    target.Add(target.Context["Provider", "Products"]);
                })

                .Having(filter => {
                    filter.Add(filter.Context["Provider", "Name"].NotLike("New"));
                })

                .OrderBy(target => {
                    target.Add(target.Context["Customer", "Name"], SortOrder.Desc);
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

        [TestMethod]
        public void Select2()
        {
            var t = new Table(typeof(Customer));
        }
        [TestMethod]
        public void Select3()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Select<Customer>(target => {
                    target += target.Context["Provider", "Products"];
                })

                .Join<Provider>(JoinType.LeftJoin, filter => {
                    filter += filter.Context["Customer", "Name"] == filter.Context["Provider", "Name"];
                })

                .Where(filter => {
                    filter += filter.Context["Customer", "City"].Like("New");
                })

                .GroupBy(target => {
                    target += target.Context["Provider", "Products"];
                })

                .Having(filter => {
                    filter += filter.Context["Provider", "Name"].NotLike("New");
                })

                .OrderBy(target => {
                    target += target.Context["Customer", "Name", SortOrder.Desc];
                    target += new KeyValuePair<Column, SortOrder>(target.Context["Customer", "Id"], SortOrder.Desc);
                });

            queryBuilder.Paginate(new Paginator());

            var query = queryBuilder.ToString();

            /*  Result:
                SELECT 
                    [Provider.Products] 
                FROM (
                    SELECT 
                        [Provider].[Products] AS [Provider.Products]
                        , ROW_NUMBER() OVER(ORDER BY [Customer].[Name] DESC, [Customer].[Id] DESC) AS [ROW_NUMBER] 
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
                ) AS [$PAGINATOR] 
                WHERE 
                    [$PAGINATOR].[ROW_NUMBER] BETWEEN 0 AND 10 
            */
        }
    }
}
