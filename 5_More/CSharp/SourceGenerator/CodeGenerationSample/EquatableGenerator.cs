using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Collections.Immutable;
using System.Text;

namespace CodeGenerationSample;

[Generator]
public class EquatableGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        static bool IsSyntaxTarget(SyntaxNode node) =>
            node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };

        static ClassDeclarationSyntax? GetTargetForGeneration(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classSyntax)
            {
                foreach (var attributeListSyntax in classSyntax.AttributeLists)
                {    
                    foreach (var attributeSyntax in attributeListSyntax.Attributes)
                    {
                        if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is IMethodSymbol methodSymbol
                                                       && methodSymbol.ContainingType.ToDisplayString() == "CodeGenerationSample.ImplementEquatableAttribute")
                        {
                            return classSyntax;
                        }
                    }
                }
            }
            return null;
        }

        static ClassDeclarationSyntax? GetTargetForGeneration1(GeneratorAttributeSyntaxContext context)
        {
            if (context.TargetNode is ClassDeclarationSyntax classSyntax)
            {
                foreach (var attributeListSyntax in classSyntax.AttributeLists)
                {
                    foreach (var attributeSyntax in attributeListSyntax.Attributes)
                    {
                        if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is IMethodSymbol methodSymbol
                                                       && methodSymbol.ContainingType.ToDisplayString() == "CodeGenerationSample.ImplementEquatableAttribute")
                        {
                            return classSyntax;
                        }
                    }
                }
            }
            return null;
        }

        static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classDeclarations, SourceProductionContext sourceContext)
        {
            if (classDeclarations.IsDefaultOrEmpty) return;

            var distinctClasses = classDeclarations.Distinct();
            if (distinctClasses is null) return;

            List<ClassToGenerate> classesToGenerate = GetTypesToGenerate(compilation, distinctClasses, sourceContext.CancellationToken);

            if (classesToGenerate.Any())
            {
                foreach (var classToGenerate in classesToGenerate)
                {
                    string result = GenerateEquatableImlementation(classToGenerate);
                    sourceContext.AddSource($"{classToGenerate}.g.cs", SourceText.From(result, Encoding.UTF8));
                }
            }
        }

        static List<ClassToGenerate> GetTypesToGenerate(Compilation compilation, IEnumerable<ClassDeclarationSyntax> classes, CancellationToken cancellationToken = default)
        {
            List<ClassToGenerate> classesToGenerate = new();

            INamedTypeSymbol? equatableAttribute = compilation.GetTypeByMetadataName("CodeGenerationSample.ImplementEquatableAttribute");

            if (equatableAttribute is null) return classesToGenerate;

            foreach (ClassDeclarationSyntax @class in classes)
            {
                cancellationToken.ThrowIfCancellationRequested();

                SemanticModel semanticModel = compilation.GetSemanticModel(@class.SyntaxTree);

                if (semanticModel.GetDeclaredSymbol(@class, cancellationToken) is INamedTypeSymbol classSymbol)
                {
                    ClassToGenerate classToGenerate = new(classSymbol.ContainingNamespace.ToDisplayString(), classSymbol.Name);
                    classesToGenerate.Add(classToGenerate);
                }
            }   

            return classesToGenerate;
        }

        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource("ImplementEquatable.g.cs", SourceText.From(attributeText, Encoding.UTF8)));

        IncrementalValuesProvider<ClassDeclarationSyntax?> classes =
        context.SyntaxProvider.ForAttributeWithMetadataName(
            "CodeGenerationSample.ImplementEquatableAttribute",
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (syntaxContext, _) => GetTargetForGeneration1(syntaxContext));

        //IncrementalValueProvider<ImmutableArray<ClassDeclarationSyntax>> classes =
        //    context.SyntaxProvider.ForAttributeWithMetadataName(
        //        "CodeGenerationSample.ImplementEquatableAttribute",
        //        predicate: static (node, _) => node is ClassDeclarationSyntax,
        //        transform: static (syntaxContext, _) => GetTargetForGeneration1(syntaxContext))
        //    .Where(c => c is not null)
        //    .Collect()!;

        //IncrementalValueProvider<ImmutableArray<ClassDeclarationSyntax>> classes = context.SyntaxProvider
        //    .CreateSyntaxProvider(
        //        predicate: static (s, _) => IsSyntaxTarget(s),
        //        transform: static (s, _) => GetTargetForGeneration(s))
        //    .Where(c => c is not null)
        //    .Collect()!;

        //IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClasses =
        //    context.CompilationProvider.Combine(classes);

        //context.RegisterSourceOutput(compilationAndClasses,
        //    static (spc, source) =>
        //    Execute(source.Item1, source.Item2, spc));
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

    public static string GenerateEquatableImlementation(ClassToGenerate classToGenerate)
    {
        string source = $$"""
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
            """;
        return source;
    }
}

public readonly record struct ClassToGenerate(string Namespace, string Name);
