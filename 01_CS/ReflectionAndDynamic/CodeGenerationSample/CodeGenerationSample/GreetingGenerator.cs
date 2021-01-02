using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeGenerationSample
{
    [Generator]
    public class GreetingGenerator : ISourceGenerator
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
            context.AddSource("AddGreetAttribute", SourceText.From(attributeText, Encoding.UTF8));

            System.Console.WriteLine("added the attribute");
            if (!(context.SyntaxReceiver is SyntaxReceiver syntaxReceiver)) 
                return;

            CSharpParseOptions? options = (context.Compilation as CSharpCompilation)?.SyntaxTrees[0].Options as CSharpParseOptions;
            Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(attributeText, Encoding.UTF8), options));

            INamedTypeSymbol? attributeSymbol = compilation.GetTypeByMetadataName("CodeGenerationSample.AddGreetAttribute");

            List<ITypeSymbol> typeSymbols = new();
            foreach (ClassDeclarationSyntax @class in syntaxReceiver.CandidateClasses)
            {
                SemanticModel model = compilation.GetSemanticModel(@class.SyntaxTree);

                ITypeSymbol? typeSymbol = model.GetDeclaredSymbol(@class) as ITypeSymbol;
                if (typeSymbol!.GetAttributes().Any(attr => attr.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
                {
                    typeSymbols.Add(typeSymbol);
                }
            }

            foreach (ITypeSymbol typeSymbol in typeSymbols)
            {
                string classSource = GetClassSource(typeSymbol);
                context.AddSource(typeSymbol.Name, SourceText.From(classSource, Encoding.UTF8));
            }
        }

        private string GetClassSource(ITypeSymbol typeSymbol)
        {
            string namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();

            StringBuilder source = new($@"
namespace {namespaceName}
{{
    public partial class {typeSymbol.Name}
    {{
        private static partial string Greet(string name)
        {{
            return $""Hello, {{name }}"";
        }}
    }}
}}
");
            return source.ToString();
        }

        private const string attributeText = @"
using System;
namespace CodeGenerationSample
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class AddGreetAttribute : Attribute
    {
        public AddGreetAttribute() { }
    }
}
";
    }

    /// <summary>
    /// Created on demand before each generation pass
    /// </summary>
    class SyntaxReceiver : ISyntaxReceiver
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
                System.Console.WriteLine($"Added {classDeclarationSyntax.Identifier} to candidates");
                CandidateClasses.Add(classDeclarationSyntax);
            }
        }
    }
}
