# Newq
New query builder for CSharp

```csharp
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
```

# License
Copyright 2015-2016 Andrew Lyu & Uriel Van

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
