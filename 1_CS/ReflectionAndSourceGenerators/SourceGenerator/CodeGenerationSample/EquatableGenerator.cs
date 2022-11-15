using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeGenerationSample;

[Generator]
public class EquatableGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        //#if DEBUG
        //            if (!Debugger.IsAttached)
        //            {
        //                Debugger.Launch();
        //            }
        //#endif
        //            Debug.WriteLine("Initialize Code Generator");
        // Register a syntax receiver that will be created for each generation pass
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        Debug.WriteLine("Execute code generator");
        // add the attribute text
        context.AddSource("ImplementEquatableAttribute", SourceText.From(attributeText, Encoding.UTF8));

        if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver)
            return;

        CSharpParseOptions? options = (context.Compilation as CSharpCompilation)?.SyntaxTrees[0].Options as CSharpParseOptions;
        Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(attributeText, Encoding.UTF8), options));

        INamedTypeSymbol? attributeSymbol = compilation.GetTypeByMetadataName("CodeGenerationSample.ImplementEquatableAttribute");

        List<INamedTypeSymbol> typeSymbols = new();
        foreach (ClassDeclarationSyntax @class in syntaxReceiver.CandidateClasses)
        {
            SemanticModel model = compilation.GetSemanticModel(@class.SyntaxTree);

            INamedTypeSymbol? typeSymbol = model.GetDeclaredSymbol(@class);
            if (typeSymbol!.GetAttributes().Any(attr => attr.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
            {
                typeSymbols.Add(typeSymbol);
            }
        }

        foreach (INamedTypeSymbol typeSymbol in typeSymbols)
        {
            string classSource = GetClassSource(typeSymbol);
            context.AddSource(typeSymbol.Name, SourceText.From(classSource, Encoding.UTF8));
        }
    }

    private string GetClassSource(ITypeSymbol typeSymbol)
    {
        string namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();

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
        return source.ToString();
    }

    private const string attributeText = """
        using System;
        namespace CodeGenerationSample;

        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        sealed class ImplementEquatableAttribute : Attribute
        {
            public ImplementEquatableAttribute() { }
        }
        """;
}

/// <summary>
/// Created on demand before each generation pass
/// </summary>
internal class SyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = new();

    /// <summary>
    /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
    /// </summary>
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // any class with at least one attribute is a candidate for the method generation
        if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax
            && classDeclarationSyntax.AttributeLists.Count > 0)
        {
            CandidateClasses.Add(classDeclarationSyntax);
        }
    }
}
