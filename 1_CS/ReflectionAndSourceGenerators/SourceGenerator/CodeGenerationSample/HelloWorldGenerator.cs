using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerationSample;

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        // Create the source code to inject into the users compilation
        StringBuilder sourceBuilder = new(
            """
            using System;
            namespace CodeGenerationSample;

            public static class HelloWorld
            {
                public static void Hello() 
                {
                    Console.WriteLine("Hello from generated code!");
                    Console.WriteLine("The following source files existed in the compilation:");
            """);          

        // using the context, get a list of files from the syntax trees in the users compilation
        IEnumerable<SyntaxTree> syntaxTrees = context.Compilation.SyntaxTrees;

        // add the filepath of each tree to the class we're building
        foreach (SyntaxTree tree in syntaxTrees)
        {
            sourceBuilder.AppendLine(
            $$"""
                  
                    Console.WriteLine(@"source file: {{tree.FilePath}}");
            """);                
        }

        // closing brackets to inject
        sourceBuilder.Append(
            """
                }
            }
            """);

        // inject the created source into the users compilation
        context.AddSource("helloWorld", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required
    }
}
