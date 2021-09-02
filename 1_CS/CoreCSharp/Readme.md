# Readme - Code Samples for Chapter 2, Core C#

**Core C#** dives into core C# features and gives you details on top-level statements and information on declaration of variables and data types. The chapter covers target-typed new expressions, explains nullable reference types, and defines a program flow that includes the new `switch` expressions.

This chapter contains the following code samples:

* TopLevelStatements (C# 9 top-level statements)
* CommandLineArgs (passing command line arguments)
* VariablesScopeSample (scope of variables)
* NullableValueTypes (nullable struct)
* NullableReferenceTypes
* ProgramFlow
* SwitchStatement
* SwitchExpression
* ForLoop (using for)
* StringSample (initializing the string type, string concatenation)

 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information on topics covered in the book.

Thank you!

## Updates with C# 10

### Implicit Namespaces

[Microsoft.Net.Sdk adds implicit namespaces](https://docs.microsoft.com/en-us/dotnet/core/compatibility/sdk/6.0/implicit-namespaces)

### Global Using Directive

[global using directive](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/globalusingdirective)

See the *StringSample* project which references `System.Text` (which is not included with implicit namespaces) using the **global using directive** (int the file GlobalUsings.cs).

### File-Scoped Namespace

[GitHub Proposal File-Scoped Namespace](https://github.com/dotnet/csharplang/issues/137)

See the *Math* project which uses a global using in the `Calculator.cs` file.