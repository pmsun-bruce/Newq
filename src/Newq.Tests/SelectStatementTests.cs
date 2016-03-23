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
                .Select<Customer>((target, context) => {
                    target.Add(context["Provider", "Products"]);
                })

                .LeftJoin<Provider>((filter, context) => {
                    filter.Add(context["Customer", "Name"].EqualTo(context["Provider", "Name"]));
                })

                .Where((filter, context) => {
                    filter.Add(context["Customer", "City"].Like("New"));
                })

                .GroupBy((target, context) => {
                    target.Add(context["Provider", "Products"]);
                })

                .Having((filter, context) => {
                    filter.Add(context["Provider", "Name"].NotLike("New"));
                })

                .OrderBy((target, context) => {
                    target.Add(context["Customer", "Name", SortOrder.Desc]);
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
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Select<Customer>((target, context) => {
                    target += context["Provider", "Products"];
                })

                .LeftJoin<Provider>((filter, context) => {
                    filter += context["Customer", "Name"] == context["Provider", "Name"];
                })

                .Where((filter, context) => {
                    filter += context["Customer", "City"].Like("New");
                })

                .GroupBy((target, context) => {
                    target += context["Provider", "Products"];
                })

                .Having((filter, context) => {
                    filter += context["Provider", "Name"].NotLike("New");
                })

                .OrderBy((target, context) => {
                    target += context["Customer", "Name", SortOrder.Desc];
                    target += context["Customer", "Id", SortOrder.Desc];
                });

            queryBuilder.Paginate(new Paginator());

            var query = queryBuilder.ToString();

            /*  Result:
                SELECT 
                    [Provider.Products] 
                FROM (
                    SELECT 
                        [Provider].[Products] AS [Provider.Products],
                        ROW_NUMBER() OVER(ORDER BY [Customer].[Name] DESC, [Customer].[Id] DESC) AS [ROW_NUMBER]
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
                    HAVING 
                        [Provider].[Name] NOT LIKE '%New%'
                ) AS [$PAGINATOR]
                WHERE
                    [$PAGINATOR].[ROW_NUMBER] BETWEEN 1 AND 10 
            */
        }
    }
}
