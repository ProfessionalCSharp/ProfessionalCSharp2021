# C# 10 and .NET 6 Updates

## Implicit Namespaces

[.NET 6 adds implicit usings](https://learn.microsoft.com/dotnet/core/project-sdk/overview#implicit-using-directives?WT.mc_id=DT-MVP-10160)

## Global Using Directive

[global using directive](https://learn.microsoft.com/dotnet/csharp/language-reference/proposals/csharp-10.0/globalusingdirective?WT.mc_id=DT-MVP-10160)

Chapter 2, StringSample

## File-Scoped Namespace

[File-Scoped Namespace](https://github.com/dotnet/csharplang/issues/137)

Chapter 3, Math Sample

## Struct

[Parameterless struct constructor](https://github.com/dotnet/csharplang/issues/99)

See chapter 3, StructsSample

## LINQ

New LINQ methods - see [LINQ](1_CS/LINQ/Readme.md)

## Collections

See chapter 8.

New `PriorityQueue` class with .NET 6 - see [Collections](1_CS/Collections/Readme.md)

## Memory

Instead of using the `Marshal` class to allocate native memory, the `NativeMemory` class can be used. See the *SpanSample* in Chapter 13, "Managed and Unmanaged Memory"

Instead of using Marshal.GetLastWin32Error, Marshal.GetLastPInvokeError can be used. See the PInvokeSampleLib in Chapter 13, "Managed and Unmanaged Memory"

## Files and Streams

.NET 6 adds an API for writeable JSON DOM. See the [Files and Streams](2_Libs/FilesAndStreams/Readme.md)

See chapter 18, *Files and Streams* for more information on JSON.

## Networking

ASP.NET Core 6 adds the `WebApplicationBuilder` and `WebApplication classes which abstracts the `Host` class. The code in the repo has been updated to use the `WebApplication` and `WebApplicationBuilder` classes.

See chapter 19, "Networking" for more information on network programming.

## Security

Sample code: **X509CertificateSample**

With .NET 6, the `Key` property of the `PublicKey` property is deprecated. The sample code is changed to use the `GetRSAPublicKey` method instead - using the `RSA` class and accessing its members.

See chapter 20, *Security* for more information on security.

## EF Core

New feature with EF Core 6:

Temporal Tables

* [Article](https://csharp.christiannagel.com/2022/01/31/efcoretemporaltables/)
* [Source Code](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/blob/main/5_More/EFCore/TemporalTableSample)

EF Core 6 has better support for *non nullable reference types*. This allows getting rid of some uses of the *null forgiving operator*, for example with [MenusContext](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021/blob/main/2_Libs/EFCore/Relationships/Menus/MenusContext.cs).

[Breaking changes with EF Core 6](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/breaking-changes) which requires a change with *owned entities* (see the Relationships sample). 
One change is that owned entities can't be nullable if they contain another owned entity and are not mapped to a different table. 
Another change is with seeding. With the Relationships sample, the Bank and Menus context use data seeding to fill initial data. With the Books context, instead of data seeding, entities are now normally added to the database which removes the requirement using anonymous types with n-to-n relations and owned entities.

See chapter 21, EF Core for more information on EF Core.

## Localization

With Windows App SDK 1.0, the namespace for MRT Core changed. See [Access app resources with MRT Core](https://docs.microsoft.com/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview#access-app-resources-with-mrt-core) and chapter 22, Localization, sample **WinUILocalization**.

## Testing with ASP.NET Core 6 using the `WebApplication` class

See chapter 23, Tests, sample ASPNETCoreSample

The Web application project needs to give access for the unit testing project (ASPNETCoreSample.csproj):

```xml
<ItemGroup>
  <InternalsVisibleTo Include="ASPNETCoreSample.IntegrationTest"/>
</ItemGroup>

See how the `WebApplicationFactory` is now used to access the `Program` class from the ASP.NET Core project in the test project *ASPNETCoreSample.IntegrationTest*

## ASP.NET Core

See the chapters 24, **ASP.NET Core"", 25 **Services**, 26 **Razor Pages and MVC**, and 27, **Blazor** for changes replacing the `Startup` class with top-level statements and the new `WebApplicationBuilder` class.
