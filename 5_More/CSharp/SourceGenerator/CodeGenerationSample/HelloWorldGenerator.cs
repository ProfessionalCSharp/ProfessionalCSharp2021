using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Text;

namespace CodeGenerationSample;

[Generator(LanguageNames.CSharp)]
public class HelloWorldGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource("HelloWorld.g.cs", SourceText.From(helloWorld, Encoding.UTF8));
        });

        var pathCollectionProvider = context.CompilationProvider.Select(static (compilation, _) =>
        {
            return compilation.SyntaxTrees.Select(tree => tree.FilePath).ToArray();
        });

        context.RegisterSourceOutput(pathCollectionProvider, (spc, content) =>
        {
            spc.AddSource("HelloWorld2.g.cs", SourceText.From(GetHelloWorld2(content), Encoding.UTF8));
        });
    }

    private const string helloWorld = """
        // <generated />

        namespace CodeGenerationSample;

        public static partial class HelloWorld
        {
            public static void Hello() => Console.WriteLine("Hello, World!");
        }
        """;

    private string GetHelloWorld2(IEnumerable<string> lines) 
    {
        string inner = string.Join("\r\n", lines.Select(line => $$"""
        Console.WriteLine(@"source file: {{line}}");
        """));

        return $$"""
        // <generated />

        namespace CodeGenerationSample;

        public static partial class HelloWorld
        {
            public static void Hello2()
            {
                Console.WriteLine("The following source files are available");
                {{inner}}
                Console.WriteLine();
            }
        }
        """;
    }
}
