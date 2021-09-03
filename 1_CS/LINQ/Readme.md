# Readme - Code Samples for Chapter 9, Language Integrated Query

**Language Integrated Query** gives you the C# language integrated query features to query data from your collections. You also learn how to use multiple CPU cores with a query and what’s behind expression trees that are used when you use LINQ to access your database with Entity Framework Core.

This chapter contains the following code samples:

* LinqIntro (showing the foundations of LINQ, showing LINQ clauses, and LINQ with extension methods)
* EnumerableSample (all the features of LINQ including filtering, grouping, joining, aggregation, lookups, using untyped collections, partitioning, compound from, and more)
* ParallelLinqSample (using parallel features including partitioners and cancellation)
* ExpressionTreeSample (working with expression trees)
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!

## Updates with C# 10

See [Updates with C# 10](../../Dotnet6Updates.md)

The *EnumerableSample* has been updated to use new LINQ methods:

* Range operator `..` with the `Take` method
* `Chunk` method 

New LINQ methods:
* TryGetNonEnumeratedCount
* ExceptBy
* IntersectBy
* UnionBy
* Chunk
