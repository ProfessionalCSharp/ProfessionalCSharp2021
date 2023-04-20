# Professional C# and .NET - 2021 Edition

Code samples for the Wrox Press book **Professional C# and .NET - 2021 Edition**

To compile and run the samples, you need Visual Studio 2019, Visual Studio for Mac, or Visual Studio Code. 

You can download these tools here: [Visual Studio](https://www.visualstudio.com/).

The console app samples can be used on Windows, Linux, or Mac. Here is the installation procedure for .NET: [.NET](https://dotnet.microsoft.com)

If you're using Visual Studio, Version 16.9 or a later version of Visual Studio 2019 is needed. With the Visual Studio Installer you need to select this workload to compile most of the code samples: **.NET Core cross-platform development**. For working with .NET 6, you should use Visual Studio 2022.

ASP.NET Core samples need this workload: **ASP.NET and web development**.

See the [WinUI.md](WinUI.md) for the requirements of building and running the WinUI samples.

## Updates, .NET 6 and .NET 7 

* See [updates](Updates.md) for issues in the book and updates. 
* See [.NET 6 Updates](Dotnet6Updates.md) for changes with .NET 6 and C# 10.
* See [.NET 7 Updates](Dotnet7Updates.md) for changes with .NET 7 and C# 11.

The *main* branch is the actual working branch with code samples updated to .NET 7 and C# 11. Every code sample should compile and run successfully (with the deadlock and race-conditions samples, *successfully* means that a deadlock and a race-condition occurs).

The *dotnet5* branch is used to show the samples how they appear in the printed book.

The *dotnet6* branch is used to give you the .NET 6 (LTS) version.

## Issues and Discussions

If you find some problems with the source code, issues to compile and run the samples, please create an [issue](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/issues). 

If you have some questions about the code samples, or just want to drop a message, you're welcome to use [Discussions](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/discussions).

For questions with your source code that are not related to the book and the code samples, it's better to use [StackOverflow](https://stackoverflow.com/).

## Slides

I'm offering training and consulting programming .NET applications and services. For academic use, slides are available for free in a separate private repository. Just send me an email using your university / high school account, tell me  how you intent to use the book to get access to the available, and send me your github account. You'll get access to this repository.

If you are interested for commercial use of the presentations, get in contact for a commercial offering.

## Source Code

Here's the list of chapters and the folders for the code samples:

### Part 1 (The Language)

* Chapter 1 - .NET Applications and Tools ([HelloWorld](1_CS/HelloWorld/Readme.md))
* Chapter 2 - Core C# ([CoreCsharp](1_CS/CoreCSharp/Readme.md))
* Chapter 3 - Classes, Structs, Tuples, and Records ([Types](1_CS/Types/Readme.md))
* Chapter 4 - Object-Oriented Programming with C# ([ObjectOrientation](1_CS/ObjectOrientation/Readme.md))
* Chapter 5 - Operators and Casts ([OperatorsAndCasts](1_CS/OperatorsAndCasts/Readme.md))
* Chapter 6 - Arrays ([Arrays](1_CS/Arrays/Readme.md))
* Chapter 7 - Delegates, Lambdas, and Events ([Delegates](1_CS/Delegates/Readme.md))
* Chapter 8 - Collections ([Collections](1_CS/Collections/Readme.md))
* Chapter 9 - Language Integrated Query ([LINQ](1_CS/LINQ/Readme.md))
* Chapter 10 - Errors and Exceptions ([ErrorsAndExceptions](1_CS/ErrorsAndExceptions/Readme.md))
* Chapter 11 - Tasks and Asynchronous Programming ([Async](1_CS/Async/Readme.md))
* Chapter 12 - Reflection, Metadata, and Source Generators ([ReflectionAndSourceGenerators](1_CS/ReflectionAndSourceGenerators/Readme.md))
* Chapter 13 - Managed and Umanaged Memory ([Memory](1_CS/Memory/Readme.md))

### Part 2  (The Libraries)

* Chapter 14 - Libraries, Assemblies, Packages, and NuGet ([Libraries](2_Libs/Libraries/Readme.md))
* Chapter 15 - Dependency Injection and Configuration ([DependencyInjectionAndConfiguration](2_Libs/DependencyInjectionAndConfiguration/Readme.md))
* Chapter 16 - Diagnostics and Metrics ([LoggingAndMetrics](2_Libs/LoggingAndMetrics))
* Chapter 17 - Parallel Programming ([Parallel](2_Libs/Parallel/Readme.md))
* Chapter 18 - Files and Streams ([FilesAndStreams](2_Libs/FilesAndStreams/Readme.md))
* Chapter 19 - Networking ([Networking](2_Libs/Networking/Readme.md))
* Chapter 20 - Security ([Security](2_Libs/Security/Readme.md))
* Chapter 21 - Entity Framework Core ([EFCore](2_Libs/EFCore/Readme.md))
* Chapter 22 - Localization ([Localization](2_Libs/Localization/Readme.md))
* Chapter 23 - Tests ([Tests](2_Libs/Tests/Readme.md))

### Part 3 (Web Apps and Services)

* Chapter 24 - ASP.NET Core ([AspNetCore](3_Web/ASPNETCore/Readme.md))
* Chapter 25 - Services ([Services](3_Web/Services/Readme.md))
* Chapter 26 - Razor Pages and MVC ([RazorAndMVC](3_Web/RazorAndMVC/Readme.md))
* Chapter 27 - Blazor ([Blazor](3_Web/Blazor/Readme.md))
* Chapter 28 - SignalR ([SignalR](3_Web/SignalR/Readme.md))

### Part 4 (Windows Apps)

* Chapter 29 - Windows Apps ([Windows](4_Apps/Windows/Readme.md))
* Chapter 30 - Patterns with XAML Apps ([Patterns](4_Apps/Patterns/Readme.md))
* Chapter 31 - Styling Windows Apps ([Styles](4_Apps/Styles/Readme.md))

### Part 5 (More Samples)

Additional articles and samples related to the book

#### The Language

* [C# Nullable Features thru the times - Article](https://csharp.christiannagel.com/2022/02/14/nullable/)
* [PriorityQueue - Source Code](5_More/Collections/PriorityQueueSample)
* [Synchronization Context - Article](https://csharp.christiannagel.com/2022/09/06/whats-the-synchronizationcontext-used-for/)
* [Synchronization Context - Source Code](5_More/Tasks/SynchronizationContext)
* [Primary Constructors - Source Code](5_More/Types/PrimaryConstructors)
* [Primary Constructors - Article](https://csharp.christiannagel.com/2023/03/28/primaryctors/)

#### The Libraries

* [Files and Streams: JSON DOM Serialization - Source Code](5_More/FilesAndStreams/JsonSample/)
* [Files and Streams:System.Text.Json Serializing Hierarchical Data - Article](https://csharp.christiannagel.com/2023/03/14/system-text-json-serializing-hierarchical-data/)
* [Files and Streams: System.Text.Json Serializing Hierarchical Data - Source Code](5_More/FilesAndStreams/JsonInheritance/)
* [Files and Streams: System.Text.Json Serializing Hierarchical Data with Source Generator](5_More/FilesAndStreams/JsonInheritanceWithSourceGenerator/)
* [EF Core: Temporal Tables - Article](https://csharp.christiannagel.com/2022/01/31/efcoretemporaltables/)
* [EF Core: Temporal Tables - Source Code](5_More/EFCore/TemporalTableSample)
* [EF Core: Using MySQL in a Docker container with EF Core - Source Code](5_More/EFCore/MySQL/)
* [EF Core: Using MySQL in a Docker container with EF Core - Article](https://csharp.christiannagel.com/2022/05/17/mysqlwithefcoreanddocker/)
* [EF Core Mapping with Generic Types, Value Conversion, and JSON Columns - Source Code](5_More/EFCore//InhertianceMappingWithConversion/)
* [EF Core Mapping with Generic Types, Value Conversion, and JSON Columns - Article](https://csharp.christiannagel.com/2023/03/07/ef-core-mapping-with-generic-types-value-conversion-and-json-columns/)

#### Web Apps and Services

* [Using Azure Active Directory B2C with .NET](https://csharp.christiannagel.com/2022/02/09/aadb2c/)
* [Upgrading an ASP.NET Core Web API Project to .NET 6 - Article](https://csharp.christiannagel.com/2022/02/22/upgrading-an-asp-net-core-web-api-project-to-net-6/)
* [Upgrading an ASP.NET Core Web API Project to .NET 6 - Source Code](5_More/Services/APIServiceUpdate/)
* [Creating a Windows Service with .NET 6 - Article](https://csharp.christiannagel.com/2022/03/22/windowsservice-2/)
* [Creating a Windows Service with .NET 6 - Source Code](5_More/WindowsServices/)
* [HTTP tools to debug Web APIs - Article](https://csharp.christiannagel.com/2023/03/21/httptools/)
* [Web API Updates with .NET 8 - Source Code](5_More/Services/TodoAPI)
* [Web API Updates with .NET 8 - Article](https://csharp.christiannagel.com/2023/04/19/api-dotnet8/)

### Windows Apps

* [Custom Behavior](5_More/WinUI/BehaviorSample)
* [XamlUiCommand](5_More/WinUI/WinUIAppEditor)

## Code of conduct

See [Code of conduct](CODE_OF_CONDUCT.md)

> Have fun coding and enjoy the book!

Christian
