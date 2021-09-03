# Readme - Code Samples for Chapter 1, .NET Applications and Tools

**.NET Applications and Tools** covers what you need to know to create .NET applications. You learn about the .NET CLI and create a Hello World application using C# top-level statements.

This chapter contains the following code samples:

* HelloWorld (.NET Core Console App)
* WebApp (Tool-generated ASP.NET Core MVC Web App)
* SelfContainedHelloWorld (Console App configured for self-contained deployment)
* TrimmedHelloWorld
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!

## Updates with C# 10

See [Updates with C# 10](../..Dotnet6Updates.md)

To see all templates available with `dotnet new`:

> dotnet new --list

### Self Contained Hello World

Page 22 - The option --self-contained is now required with .NET 6:

```
dotnet publish --self-contained -c Release -r win10-x64
dotnet publish --self-contained -c Release -r osx.10.11-x64
dotnet publish --self-contained -c Release -r ubuntu-x64
```
