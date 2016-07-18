# Newq

[![Build Status][travis-image]][travis-url]
[![NuGet Version][nuget-image]][nuget-url]

A new query builder for CSharp.

## Usage

```csharp
var queryBuilder = new QueryBuilder();

queryBuilder
    .Select<Customer>((target, context) => {
        target += context["Provider", "Products"];
    })

    .LeftJoin<Provider>((filter, context) => {
        filter += context["Customer", "Name"].EqualTo(context["Provider", "Name"]);
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
    });

    queryBuilder.Paginate(new Paginator());

    var result = queryBuilder.ToString();

    /* result:

    SELECT
        [Provider.Products]
        ,[Customer.Name]
    FROM (
        SELECT
            ROW_NUMBER() OVER(ORDER BY [Customer.Name] DESC) AS [$ROW_NUMBER]
            ,[Provider.Products]
            ,[Customer.Name]
        FROM (
            SELECT
                [Provider].[Products] AS [Provider.Products]
                ,[Customer].[Name] AS [Customer.Name]
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
        ) AS [$ORIGINAL_QUERY]
    ) AS [$PAGINATOR]
    WHERE
        [$PAGINATOR].[$ROW_NUMBER] BETWEEN 1 AND 10

    */
```

## Installation

To install, run the following command in the `Package Manager Console`:

```
PM> Install-Package Newq
```

## License

```
Copyright 2015-2016 Andrew Lyu and Uriel Van

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```

[travis-image]: https://travis-ci.org/apemost/Newq.svg?branch=master
[travis-url]: https://travis-ci.org/apemost/Newq
[nuget-image]: http://img.shields.io/nuget/v/Newq.svg?style=flat
[nuget-url]: https://www.nuget.org/packages/Newq/
