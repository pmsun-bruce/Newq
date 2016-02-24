namespace Newq.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newq;

    [TestClass]
    public class QueryBuilderTest
    {
        [TestMethod]
        public void Select()
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

        [TestMethod]
        public void Update()
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
        public void Insert()
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

        [TestMethod]
        public void Delete()
        {
            var queryBuilder = new QueryBuilder();

            queryBuilder
                .Delete<Customer>()
                .Where(con => {
                    var cust = con.DbContext["Customer"];

                    con.Add(cust["City"].Between("New York", "Landon"));
                    con.Add(cust["Name"].Like("Google").Or(cust["Name"].BeginWith("Apple")));
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

    public class Customer : Model
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class Provider : Model
    {
        public string Name { get; set; }
        public string Products { get; set; }
    }
}
