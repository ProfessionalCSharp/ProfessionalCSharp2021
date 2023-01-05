# Readme - Code Samples for Chapter 12, Reflection, Metadata, and Source Generators

**Reflection, Metadata, and Source Generators** covers using and reading attributes with C#. The attributes will not just be read using reflection, but you’ll also see the functionality of source generators that allow creating source code during compile time.

This chapter contains the following code samples:

* Reflection
    * LookupWhatsNew (Reading custom attributes dynamically)
    * TypeView (Use reflection to get information about types)
    * VectorClass (Library, custom attributes annotated)
    * WhatsNewAttributes (Library, defines custom attributes) 
* Dynamic Programming
    * CalculatorLib (Library that will be loaded dynamically)
    * DynamicSamples (a custom class using *DynamicObject*)
    * ClientApp (console app loading CalculatorLib dynamically)
    * DynamicFileReader (parsing a file and making use of *dynamic*)
* Source Generator (uses a NuGet package)
    * CodeGenerationSample
    * SampleApp

## Additional information on source generators (use as NuGet package):

To automatically create a NuGet package on build (see folder `SourceGeneratorAsNuGet`):

```xml
<PropertyGroup>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  <IncludeBuildOutput>false</IncludeBuildOutput> <!-- Do not include as a lib dependency -->
</PropertyGroup>
```

To use the source generator a a NuGet package, copy the generated NuGet package to a folder such as c:\mypackages, and reference this package from the Visual Studio package manager.

## .NET 7 Update

The source generator sample makes use of **raw string literals**. This makes the code more readable, and the code is easier to maintain. 

This is a code snippet from C# 10:

```csharp
            StringBuilder source = new($@"
using System;
namespace {namespaceName}
{{
    public partial class {typeSymbol.Name} : IEquatable<{typeSymbol.Name}>
    {{
        private static partial bool IsTheSame({typeSymbol.Name}? left, {typeSymbol.Name}? right);
        public override bool Equals(object? obj) => this == obj as {typeSymbol.Name};
        public bool Equals({typeSymbol.Name}? other) => this == other;
        public static bool operator==({typeSymbol.Name}? left, {typeSymbol.Name}? right) => 
            IsTheSame(left, right);
        public static bool operator!=({typeSymbol.Name}? left, {typeSymbol.Name}? right) =>
            !(left == right);
    }}
}}
");
```

And this is the version for C# 11:

```csharp
string source = $$"""
    using System;

    namespace {{namespaceName}};

    public partial class {{typeSymbol.Name}} : IEquatable<{{typeSymbol.Name}}>
    {
        private static partial bool IsTheSame({{typeSymbol.Name}}? left, {{typeSymbol.Name}}? right);

        public override bool Equals(object? obj) => this == obj as {{typeSymbol.Name}};

        public bool Equals({{typeSymbol.Name}}? other) => this == other;

        public static bool operator==({{typeSymbol.Name}}? left, {{typeSymbol.Name}}? right) => 
            IsTheSame(left, right);

        public static bool operator!=({{typeSymbol.Name}}? left, {{typeSymbol.Name}}? right) =>
            !(left == right);
    }
    """;
```


## More Information
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!

## Updates with C# 10 and C# 11

* See [Updates with C# 10 and .NET 6](../../Dotnet6Updates.md)
* See [Updates with C# 11 and .NET 7](../../Dotnet7Updates.md)


