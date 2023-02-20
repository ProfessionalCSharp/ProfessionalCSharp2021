using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Text;

namespace CodeGenerationSample;

[Generator]
public class EquatableGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        static ClassToGenerateInfo? GetClassToGenerateInfo(GeneratorAttributeSyntaxContext context)
        {
            if (context.TargetNode is ClassDeclarationSyntax classSyntax)
            {
                foreach (var attributeListSyntax in classSyntax.AttributeLists)
                {
                    foreach (var attributeSyntax in attributeListSyntax.Attributes)
                    {
                        SymbolInfo symbolInfo = context.SemanticModel.GetSymbolInfo(attributeSyntax);

                        ISymbol? symbol = symbolInfo.Symbol ?? symbolInfo.CandidateSymbols.FirstOrDefault();
                        if (symbol is not IMethodSymbol methodSymbol)
                        {
                            continue;
                        }
                        if (methodSymbol.ContainingType is INamedTypeSymbol namedTypeSymbol)
                        {
                            string fullAttributeName = namedTypeSymbol.ToDisplayString();
                            if (fullAttributeName == "CodeGenerationSample.ImplementEquatableAttribute")
                            {
                                string className = classSyntax.Identifier.ValueText;
                                string? namespaceName = classSyntax.Parent switch
                                {
                                    NamespaceDeclarationSyntax nsds => nsds.Name.ToString(),
                                    FileScopedNamespaceDeclarationSyntax fsnds => fsnds.Name.ToString(),
                                    _ => null
                                };

                                return new ClassToGenerateInfo(className, namespaceName);
                            }
                        }
                    }
                }
            }
            return null;
        }

        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource("ImplementEquatable.g.cs", SourceText.From(attributeText, Encoding.UTF8)));

        IncrementalValuesProvider<ClassToGenerateInfo?> classes =
        context.SyntaxProvider.ForAttributeWithMetadataName(
            "CodeGenerationSample.ImplementEquatableAttribute",
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (syntaxContext, _) => GetClassToGenerateInfo(syntaxContext));

        context.RegisterSourceOutput(classes, (context, ctg) =>
        {
            if (ctg is not null)
            {
                context.AddSource($"{ctg.Value.Name}.g.cs", SourceText.From(GenerateEquatableImlementation(ctg.Value), Encoding.UTF8));
            }

        });
    }

    private const string attributeText = """
        // <generated />
        using System;
        namespace CodeGenerationSample;

        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        sealed class ImplementEquatableAttribute : Attribute
        {
            public ImplementEquatableAttribute() { }
        }
        """;

    public static string GenerateEquatableImlementation(ClassToGenerateInfo classToGenerate)
    {
        string source = $$"""
            // <generated />

            #nullable enable
            using System;

            namespace {{classToGenerate.Namespace}};

            public partial class {{classToGenerate.Name}} : IEquatable<{{classToGenerate.Name}}>
            {
                private static partial bool IsTheSame({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right);

                public override bool Equals(object? obj) => this == obj as {{classToGenerate.Name}};

                public bool Equals({{classToGenerate.Name}}? other) => this == other;

                public static bool operator==({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right) => 
                    IsTheSame(left, right);

                public static bool operator!=({{classToGenerate.Name}}? left, {{classToGenerate.Name}}? right) =>
                    !(left == right);
            }

            #nullable restore
            """;
        return source;
    }
}

public readonly record struct ClassToGenerateInfo(string Name, string? Namespace = default);
