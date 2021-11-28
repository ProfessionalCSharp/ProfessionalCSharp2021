# C# 10 and .NET 6 Updates

## Implicit Namespaces

[Microsoft.Net.Sdk adds implicit namespaces](https://docs.microsoft.com/en-us/dotnet/core/compatibility/sdk/6.0/implicit-namespaces)

## Global Using Directive

[global using directive](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/globalusingdirective)

Chapter 2, StringSample

## File-Scoped Namespace

[File-Scoped Namespace](https://github.com/dotnet/csharplang/issues/137)

Chapter 3, Math Sample

## Struct

[Parameterless struct constructor](https://github.com/dotnet/csharplang/issues/99)

See chapter 3, StructsSample

## LINQ

New LINQ methods - see [LINQ](1_CS/LINQ/Readme.md)

## Memory

Instead of using the `Marshal` class to allocate native memory, the `NativeMemory` class can be used. See the *SpanSample* in Chapter 13, "Managed and Unmanaged Memory"

Instead of using Marshal.GetLastWin32Error, Marshal.GetLastPInvokeError can be used. See the PInvokeSampleLib in Chapter 13, "Managed and Unmanaged Memory"

> WinUI samples will be updated to .NET 6/C# 10 syntax at a later time

## Testing with ASP.NET Core 6 using the `WebApplication` class

See chapter 23, Tests, sample ASPNETCoreSample

The Web application project needs to give access for the unit testing project (ASPNETCoreSample.csproj):

```xml
	<ItemGroup>
		<InternalsVisibleTo Include="ASPNETCoreSample.IntegrationTest"/>
	</ItemGroup>
```

See how the `WebApplicationFactory` is now used to access the `Program` class from the ASP.NET Core project in the test project *ASPNETCoreSample.IntegrationTest*
