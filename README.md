# Newq
A new query builder for CSharp

# Contains:
1. QueryBuilder

```csharp
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

var query = queryBuilder.ToString();
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
