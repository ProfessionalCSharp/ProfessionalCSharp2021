# Readme - Code Samples for Chapter 21, Entity Framework Core

**Entity Framework Core** covers reading and writing data from a database—including the many features offered from EF Core, such as shadow properties, global query filters, many-to-many relations, and what metric information is now offered by EF Core—including and reading and writing to Azure Cosmos DB with EF Core.

The sample code for this chapter contains of these sample projects:

* Intro (introduction to EF Core with models, classes and records, contexts, creating the database, read, write, update, delete, logging)
* Models (relations, mapping, self contained type configuration, mapping to fields, shadow properties)
* ScaffoldSmaple (using `dotnet ef` for scaffolding)
* MigrationsApp (migrations)
* Queries (basic queries, asynchronous streams, raw SQL queries, compiled queries, global query filters, EF.functions)
* LoadedRelatedData (eager loading, filtered include, explicit loading, lazy loading)
* Relationships (many-to-many, table splitting, owned entities, table per hierarchy)
* Tracking (tracking ojects, updating objects)
* ConflictHandling-LastWins
* ConflictHandling-FirstWins
* Transactions (implicit transactions, explicit transactions, ambient transactions)
* Cosmos (using Cosmos DB)

## Additional Samples

[Temporal Tables with EF Core](https://csharp.christiannagel.com/2022/01/31/efcoretemporaltables/)

## Create Azure SQL Database

See this script to create an Azure SQL database: [createazuresql.sh](createazuresql.sh)

## Tools

To access the database, you can use the **Azure Data Studio** on Windows, MacOS, and Linux:
[Download and install Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)
 
## More Information

For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!