# Readme - Code Samples for Chapter 25, Services

**Services** dives into creating microservices using different technologies such as ASP.NET Core as well as using Azure Functions and gRPC for binary communication.

This chapter contains these samples:

* BooksApi (ASP.NET Core Web API and a .NET client)
* Books Data (ASP.NET Core Web API with EF Core)
* BooksApiWithB2C (ASP.NET Core Web API with Azure Active Directory B2C)
* GRPC (GRPC service and client)
* Azure Functions

## Azure Active Directory B2C

For the sample *BooksApiWithB2C*, create an Azure Active Directory B2C tenant using your Microsoft Azure subscription.

## Azure Functions

To use Azure Functions with .NET 5, install the *Azure Functions Core Tools*

* [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)

At the time of this writing, you can't start the Azure Function with .NET 5 from Visual Studio. Instead, use the Azure Functions Core Tools. 

To create a .NET 5 Azure Functions project use:

> func init --worker-runtime dotnetIsolated

For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!