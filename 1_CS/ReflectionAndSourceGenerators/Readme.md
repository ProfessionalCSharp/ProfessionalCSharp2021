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
* Source Generator
    * CodeGenerationSample
    * SampleApp
* SourceGeneratorAsNuGet (configure the source generator package to be used as a NuGet package and not as analyzer)

## Additional information on source generators (use as NuGet package):

To automatically create a NuGet package on build (see folder `SourceGeneratorAsNuGet`):

```xml
<PropertyGroup>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  <IncludeBuildOutput>false</IncludeBuildOutput> <!-- Do not include as a lib dependency -->
</PropertyGroup>
```

Adding the library to the `analyzers/dotnet/cs` folder, the source generator is automatically used as analyzer :

```xml
<ItemGroup>
  <None Include="$(OutputPath)\$(AssemblyName).dll" Pack="true" PackagePath="analyzers/dotnet/cs" Visible="false" />
</ItemGroup>
```

## More Information
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!